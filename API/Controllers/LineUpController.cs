using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Controllers.DTOs;
using API.Core;
using API.Core.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Route("api/lineUp")]
    [ApiController]
    // [Authorize]
    public class LineUpController : ControllerBase
    {
        private IMapper _mapper { get; }
        private ILineUpRepository _lineUpRepository { get; }
        private IUserRepository _userRepository { get; }

        public LineUpController(ILineUpRepository lineUpRepository, IMapper mapper, IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _lineUpRepository = lineUpRepository;
            _mapper = mapper;
        }

        [HttpPost("createSpace/{userId}")]
        public async Task<IActionResult> CreateSpace(int userId, [FromBody] SpaceDTO spaceDTO)
        {
            // if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value) || (User.FindFirst(ClaimTypes.Role).Value != Role.AnySpaces.ToString()) || (User.FindFirst(ClaimTypes.Role).Value != Role.Merchant.ToString())) {
            //     return Unauthorized();
            // }
            var user = await _userRepository.GetUser(spaceDTO.User.Id);
            var noOfSpaces = await _userRepository.GetNoOfSpaces(spaceDTO.User.Id);
            if(noOfSpaces >= 2 && !user.VerifiedAsMerchant) {
                return BadRequest("Only verified merchants can create more than two spaces, please verify your account");
            }
            var spaceToCreate = _mapper.Map<Space>(spaceDTO);
            if (spaceToCreate == null)
                return BadRequest("Space cannot be null");
            if (await _lineUpRepository.EntityExists(spaceToCreate))
                return BadRequest("Space has already been created");
            spaceToCreate.DateCreated = DateTime.Now;
            _lineUpRepository.Add(spaceToCreate);
            await _lineUpRepository.SaveAllChanges();
            return Ok();
        }
        [HttpPost("createAmenities/{userId}")]
        public async Task<IActionResult> CreateAmenities(int userId, [FromBody] AmenitiesToCreate amenitiesDTO)
        {
            // if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value) || (User.FindFirst(ClaimTypes.Role).Value != Role.AnySpaces.ToString()) || (User.FindFirst(ClaimTypes.Role).Value != Role.Merchant.ToString())) {
            //     return Unauthorized();
            // }
            var space = await _lineUpRepository.GetSpace(amenitiesDTO.SpaceId);
            space.Amenities = new Collection<Amenity>();
            await _lineUpRepository.SaveAllChanges();
            foreach (var amenity in amenitiesDTO.Amenities)
            {
                var amenityToCreate = _mapper.Map<Amenity>(amenity);
                amenity.SpaceId = amenitiesDTO.SpaceId;

                if (amenityToCreate == null)
                    return BadRequest("Amenity cannot be null");

                // if (await _lineUpRepository.EntityExists(amenityToCreate))
                //     return BadRequest("Space has already been created");
                space.Amenities.Add(amenityToCreate);
            }
            await _lineUpRepository.SaveAllChanges();
            return Ok();
        }
        [HttpGet("getSpace/{spaceId}")]
        public async Task<IActionResult> GetSpace(int spaceId)
        {
            var space = await _lineUpRepository.GetSpace(spaceId);
            if (space == null)
                return NotFound("Space does not exist");
            // var spaceToReturn = _mapper.Map<SpaceToEdit>(space);
            // spaceToReturn.SelectedPricingOption = spaceToReturn.SelectedPricingOption.ToString();
            return Ok(space);
        }
        [HttpPut("updateSpace/{spaceId}")]
        public async Task<IActionResult> UpdateSpace(int spaceId, [FromBody] SpaceDTO spaceDTO)
        {
            // if (spaceDTO.UserId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value) || (User.FindFirst(ClaimTypes.Role).Value != Role.AnySpaces.ToString()) || (User.FindFirst(ClaimTypes.Role).Value != Role.Merchant.ToString())) {
            //     return Unauthorized();
            // }
            var space = await _lineUpRepository.GetSpace(spaceId);
            var amenities = new List<Amenity>();
            var photos = new List<Photo>();
            foreach (var amenity in space.Amenities)
            {
                amenities.Add(amenity);
            }
            foreach (var photo in space.Photos)
            {
                photos.Add(photo);
            }
            // var photos = space.Photos;
            // _lineUpRepository.Delete(space.Amenities);
            var spaceToUpdate = _mapper.Map<SpaceDTO, Space>(spaceDTO, space);
            if (spaceToUpdate == null)
                return BadRequest("Space cannot be null");
            if (await _lineUpRepository.EntityExists(spaceToUpdate))
            {
                // spaceToUpdate.Amenities.Entr
                spaceToUpdate.Photos = photos;
                spaceToUpdate.Amenities = amenities;
                _lineUpRepository.Update(spaceToUpdate);
                await _lineUpRepository.SaveAllChanges();
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("deleteSpace/{spaceId}")]
        public async Task<IActionResult> DeleteSpace(int spaceId)
        {
            var spaceToDispose = await _lineUpRepository.GetSpace(spaceId);
            // if (spaceToDispose.UserId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value) || (User.FindFirst(ClaimTypes.Role).Value != Role.AnySpaces.ToString()) || (User.FindFirst(ClaimTypes.Role).Value != Role.Merchant.ToString())) {
            //     return Unauthorized();
            // }
            if (spaceToDispose != null)
            {
                _lineUpRepository.Delete(spaceToDispose);
                await _lineUpRepository.SaveAllChanges();
                return Ok(spaceId);
            }
            return NotFound();
        }

        [HttpGet("getSpaces")]
        public async Task<QueryResult<Space>> GetSpaces([FromQuery] SpaceQueryDTO queryDTO)
        {
            var query = _mapper.Map<SpaceQuery>(queryDTO);
            var spacesQueryResult = await _lineUpRepository.GetSpaces(query);
            return spacesQueryResult;
        }
        [HttpPost("getBookedTimes/{spaceId}")]
        public async Task<IActionResult> GetBookedTimes(int spaceId, [FromBody] BookedTimes pBT)
        {
            var bookedTimes = await _lineUpRepository.GetBookedTimes(spaceId, pBT);
            return Ok(bookedTimes);
        }
        [HttpGet("getMerchantSpaces")]
        public async Task<IActionResult> GetMerchantSpaces([FromQuery] SpaceQueryDTO queryDTO)
        {
            // if (queryDTO.UserId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value) || (User.FindFirst(ClaimTypes.Role).Value != Role.AnySpaces.ToString()) || (User.FindFirst(ClaimTypes.Role).Value != Role.Merchant.ToString())) {
            //     return Unauthorized();
            // }
            var query = _mapper.Map<SpaceQuery>(queryDTO);
            var spacesQueryResult = await _lineUpRepository.GetMerchantSpaces(query);
            return Ok(spacesQueryResult);
        }

        [HttpGet("getSpaceTypes")]
        public async Task<IEnumerable<SpaceType>> GetSpaceTypes()
        {
            var spaceTypes = await _lineUpRepository.GetSpaceTypes();
            return spaceTypes;
        }
        // [HttpGet("getPricingOptions")]
        // public async Task<IEnumerable<PricingOption>> GetPricingOptions()
        // {
        //     var pricingOptions = await _lineUpRepository.GetPricingOptions();
        //     return pricingOptions;
        // }
        [HttpPost("createSpaceType/{userId}")]
        public async Task<IActionResult> CreateSpaceType(int userId, [FromBody] SpaceTypeDTO spaceTypeDTO)
        {
            // if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value) || (User.FindFirst(ClaimTypes.Role).Value != Role.AnySpaces.ToString()) || (User.FindFirst(ClaimTypes.Role).Value != Role.Merchant.ToString())) {
            //     return Unauthorized();
            // }
            var spaceTypeToCreate = _mapper.Map<SpaceType>(spaceTypeDTO);
            if (spaceTypeToCreate == null)
                return BadRequest("SpaceType cannot be null");
            if (await _lineUpRepository.EntityExists(spaceTypeToCreate))
                return BadRequest("Space type already exists");
            _lineUpRepository.Add(spaceTypeToCreate);
            await _lineUpRepository.SaveAllChanges();
            return Ok();
        }

        [HttpPut("updateSpaceType/{spaceTypeId}")]
        public async Task<IActionResult> UpdateSpaceType(int spaceTypeId, [FromBody] SpaceTypeDTO spaceTypeDTO)
        {
            var spaceType = await _lineUpRepository.GetSpaceType(spaceTypeId);
            var spaceTypeToUpdate = _mapper.Map<SpaceTypeDTO, SpaceType>(spaceTypeDTO, spaceType);
            if (spaceTypeToUpdate == null)
                return BadRequest("Spacetype cannot be null");
            if (await _lineUpRepository.EntityExists(spaceTypeToUpdate) == false)
                return BadRequest("Spacetype does not exist");
            _lineUpRepository.Update(spaceTypeToUpdate);
            await _lineUpRepository.SaveAllChanges();
            return Ok();
        }

        [HttpDelete("deleteSpaceType/{spaceTypeId}")]
        public IActionResult DeleteSpaceType(int spaceTypeId)
        {
            // if ((User.FindFirst(ClaimTypes.Role).Value != Role.AnySpaces.ToString()) || (User.FindFirst(ClaimTypes.Role).Value != Role.Merchant.ToString())) {
            //     return Unauthorized();
            // }
            var spaceTypeToDispose = _lineUpRepository.GetSpaceType(spaceTypeId);
            if (spaceTypeToDispose != null)
            {
                _lineUpRepository.Delete(spaceTypeToDispose);
                _lineUpRepository.SaveAllChanges();
                return Ok();
            }
            return NotFound();
        }

        [HttpPost("createAmenity")]
        public async Task<IActionResult> CreateAmenity([FromBody] AmenityDTO amenityDTO)
        {
            // if ((User.FindFirst(ClaimTypes.Role).Value != Role.AnySpaces.ToString()) || (User.FindFirst(ClaimTypes.Role).Value != Role.Merchant.ToString())) {
            //     return Unauthorized();
            // }
            var amenityToCreate = _mapper.Map<Amenity>(amenityDTO);
            
            if (amenityToCreate == null)
                return BadRequest("Amenity cannot be null");
            if (await _lineUpRepository.EntityExists(amenityToCreate))
                return BadRequest("Amenity already exists");

            _lineUpRepository.Add(amenityToCreate);
            await _lineUpRepository.SaveAllChanges();
            return Ok();
        }

        // [HttpPost("createPricingOption")]
        // public async Task<IActionResult> CreatePricingOption([FromBody] PricingOption pricingOptionDTO)
        // {
        //     var pricingOptionToCreate = _mapper.Map<PricingOption>(pricingOptionDTO);

        //     if(pricingOptionToCreate == null)
        //         return BadRequest("Pricing option cannot be null");
        //     if(await _lineUpRepository.EntityExists(pricingOptionToCreate))
        //             return BadRequest("Pricing option already exists");

        //     _lineUpRepository.Add(pricingOptionToCreate);
        //     await _lineUpRepository.SaveAllChanges();
        //     return Ok();
        // }

        [HttpPut("updateAmenity/{amenityId}")]
        public async Task<IActionResult> UpdateAmenity(int amenityId, [FromBody] AmenityDTO amenityDTO)
        {
            // if ((User.FindFirst(ClaimTypes.Role).Value != Role.AnySpaces.ToString()) || (User.FindFirst(ClaimTypes.Role).Value != Role.Merchant.ToString())) {
            //     return Unauthorized();
            // }
            var amenity = await _lineUpRepository.GetAmenity(amenityId);
            var amenityToUpdate = _mapper.Map<AmenityDTO, Amenity>(amenityDTO, amenity);
            if (amenityToUpdate == null)
                return BadRequest("Amenity cannot be null");
            if (await _lineUpRepository.EntityExists(amenityToUpdate) == false)
                return BadRequest("Amenity does not exist");
            _lineUpRepository.Update(amenityToUpdate);
            return Ok();
        }
        [HttpPost("spaceEnquiry")]
        public async Task<IActionResult> SendUsAMessage([FromBody] YourMessage yourMessage)
        {
            var user = await _userRepository.GetUser(yourMessage.SpaceOwnerId);
            Message message = new Message();
            message.Subject = "Enquiry";
            message.FromEmail = "enquiry@234spaces.com";
            message.FromName = "234Spaces Enquiry";
            message.ToName = user.Name;
            message.ToEmail = user.Email;
            message.PlainContent = null;
            message.HtmlContent = FormattedEmailBody("Space Enquiry", $"An enquiry was logged on your space, the details are as follows. Enquiry made by {yourMessage.Name}. Message is {yourMessage.Message}. Phone number is {yourMessage.Phone}. Email is {yourMessage.Email}, and registered email on 234Spaces is {yourMessage.User234SpacesEmail}", "", "", false);
            _userRepository.EmailSender(message);
            return Ok();
        }

        [HttpPost("createReservationPrev")]
        public async Task<IActionResult> CreateReservahshtion([FromBody] BookingDTO bookingDTO)
        {
            var bookingToCreate = _mapper.Map<Booking>(bookingDTO);
            if (bookingToCreate == null)
                return BadRequest("Booking cannot be null");
            if (bookingToCreate.UsingFrom < DateTime.Now)
                return BadRequest("You can't create a booking for a date that has passed");
            if (bookingToCreate.UsingFrom >= bookingToCreate.UsingTill)
                return BadRequest("Time from must be behind/be the same as time to");
            if (await _lineUpRepository.EntityExists(bookingToCreate))
                return BadRequest("Booking already exists");
            var bookingToCreateBT = new BookedTimes();
            bookingToCreateBT.From = bookingToCreate.UsingFrom;
            bookingToCreateBT.To = bookingToCreate.UsingTill;
            // var existingBookingTimes = await _lineUpRepository.GetBookedTimes(bookingToCreate.SpaceBookedId, bookingToCreateBT);
            // if (existingBookingTimes.Count() > 0)
            //     return BadRequest("You can't select from a range of booking that already exists");
            bookingToCreate.Status = BookingStatus.Reserved;
            bookingToCreate.BookingTime = DateTime.Now;
            _lineUpRepository.Add(bookingToCreate);
            await _lineUpRepository.SaveAllChanges();

            BookingQuery query = new BookingQuery {
                TimeBooked = bookingToCreate.BookingTime
            };
            var bookingDetails = await _lineUpRepository.GetBookingDetails(bookingToCreate.BookedById, query);

            var user = await _userRepository.GetUser(bookingToCreate.BookedById);
            Message message = new Message();
            message.Subject = "Reservation";
            message.FromEmail = "noreply@234spaces.com";
            message.FromName = "234Spaces Admin";
            message.ToName = user.Name;
            message.ToEmail = user.Email;
            message.PlainContent = null;
            if(bookingDetails.Count() > 1) {
                var reservationIdList = String.Empty;
                var totalPrice = 0.00;
                foreach (var item in bookingDetails)
                {
                    reservationIdList += item.Id.ToString() + " ,";
                    totalPrice  += item.TotalPrice;
                }
                message.HtmlContent = FormattedEmailBody("Your Reservations", $"The following reservations has been placed for you, your reservation references are {reservationIdList.ToString()}, kindly pay #{totalPrice.ToString()} to confirm the reservation", "", "", false);
            } else {
                message.HtmlContent = FormattedEmailBody("Your Reservation", $"A reservation has been placed for you, your reservation reference is {bookingDetails[0].Id.ToString()}, kindly pay #{bookingDetails[0].TotalPrice.ToString()} to confirm the reservation", "", "", false);
            }
            _userRepository.EmailSender(message);
            return Ok();
        }

        [HttpPost("createBooking")]
        public async Task<IActionResult> CreateBooking([FromBody] BookingFromClientDTO bookingFromClientDTO)
        {
            var bookingDTO = new BookingDTO {
                // SpaceBookedId = bookingFromClientDTO.SpaceBookedId,
                // UserId = bookingFromClientDTO.UserId,
                BookingTime = bookingFromClientDTO.BookingTime,
                TotalPrice = bookingFromClientDTO.TotalPrice,
                Chat = bookingFromClientDTO.Chat,
                BookedById = bookingFromClientDTO.BookedById,
                Status = bookingFromClientDTO.Status,
                NoOfGuests = bookingFromClientDTO.NoOfGuests,
                BookedForEmail = bookingFromClientDTO.BookedForEmail,
                BookedForName = bookingFromClientDTO.BookedForName,
                BookedForPhone = bookingFromClientDTO.BookedForPhone
            };
            var bookingToCreate = new Booking();
            foreach (var time in bookingFromClientDTO.UsingTimes)
            {
                bookingToCreate.UsingFrom = time.UsingFrom;
                bookingToCreate.UsingTill = time.UsingTill;
                var bookingToCreateBT = new BookedTimes();
                bookingToCreateBT.From = bookingToCreate.UsingFrom;
                bookingToCreateBT.To = bookingToCreate.UsingTill;
                var existingBookingTimes = await _lineUpRepository.GetBookedTimes(bookingToCreate.IdOfSpaceBooked, bookingToCreateBT);
                if (existingBookingTimes.Count() > 0)
                    return BadRequest("You can't select from a range of booking that already exists");
                if (bookingToCreate == null)
                    return BadRequest("Booking cannot be null");
                if (bookingToCreate.UsingFrom < DateTime.Now)
                    return BadRequest("You can't create a booking for a date that has passed");
                if (bookingToCreate.UsingFrom >= bookingToCreate.UsingTill)
                    return BadRequest("Time from must be behind/be the same as time to");
                if (await _lineUpRepository.EntityExists(bookingToCreate))
                    return BadRequest("Booking already exists");
                bookingToCreate.Status = BookingStatus.Booked;
                bookingToCreate.NoOfGuests = bookingDTO.NoOfGuests;
                bookingToCreate.BookingTime = DateTime.Now;
                _lineUpRepository.Add(bookingToCreate);
            }
            await _lineUpRepository.SaveAllChanges();
            BookingQuery query = new BookingQuery {
                TimeBooked = bookingToCreate.BookingTime.Date
            };
            var user = await _userRepository.GetUser(bookingToCreate.BookedById);
            var bookingDetails = await _lineUpRepository.GetBookingDetails(bookingToCreate.BookedById, query);
            Message message = new Message();
            message.Subject = "Booking";
            message.FromEmail = "noreply@234spaces.com";
            message.FromName = "234Spaces Admin";
            message.ToName = user.Name;
            message.ToEmail = user.Email;
            message.PlainContent = null;
            if(bookingDetails.Count() > 1) {
                var reservationIdList = String.Empty;
                var totalPrice = 0.00;
                foreach (var item in bookingDetails)
                {
                    reservationIdList += item.BookingRef.ToString() + " ,";
                    totalPrice  += item.TotalPrice;
                }
                message.HtmlContent = FormattedEmailBody("Your Booking", $"The following bookings has been placed for you, your booking references are {reservationIdList.ToString()}.", "", "", false);
            } else {
                message.HtmlContent = FormattedEmailBody("Your Booking", $"Your booking has been confirmed, your bookingReference is {bookingDetails[0].BookingRef.ToString()}", "", "", false);
            }
            _userRepository.EmailSender(message);

            return Ok();
        }
        [HttpGet("getUser/{userId}")]
        public async Task<IActionResult> GetUser(int userId)
        {
            var user = await _lineUpRepository.GetUser(userId);
            var userToReturn  = _mapper.Map<UserToReturnDTO>(user);
            return Ok(userToReturn);
        }


        [HttpPost("createReservation")]
        public async Task<IActionResult> CreateReservation([FromBody] BookingFromClientDTO bookingFromClientDTO)
        {
            var user = await _userRepository.GetUser(bookingFromClientDTO.BookedById);
            // var user = await _lineUpRepository.GetUser(bookingFromClientDTO.BookedById);
            var bookingDTO = new BookingDTO {
                // SpaceBookedId = bookingFromClientDTO.SpaceBookedId,
                // UserId = bookingFromClientDTO.UserId,
                BookingTime = bookingFromClientDTO.BookingTime,
                TotalPrice = bookingFromClientDTO.TotalPrice,
                Chat = bookingFromClientDTO.Chat,
                IdOfSpaceBooked = bookingFromClientDTO.IdOfSpaceBooked,
                BookedById = bookingFromClientDTO.BookedById,
                Status = bookingFromClientDTO.Status,
                NoOfGuests = bookingFromClientDTO.NoOfGuests,
                BookedForEmail = bookingFromClientDTO.BookedForEmail,
                BookedForName = bookingFromClientDTO.BookedForName,
                BookedForPhone = bookingFromClientDTO.BookedForPhone
            };
            var timeOfCreation = new DateTime();
            foreach (var time in bookingFromClientDTO.UsingTimes)
            {
                var bookingToCreate = new Booking();
                bookingToCreate.UsingFrom = time.UsingFrom;
                bookingToCreate.UsingTill = time.UsingTill;
                var bookingToCreateBT = new BookedTimes();
                bookingToCreateBT.From = bookingToCreate.UsingFrom;
                bookingToCreateBT.To = bookingToCreate.UsingTill;
                // var existingBookingTimes = await _lineUpRepository.GetBookedTimes(bookingToCreate.SpaceBooked.Id, bookingToCreateBT);
                // if (existingBookingTimes.Count() > 0)
                //     return BadRequest("You can't select from a range of booking that already exists");
                if (bookingToCreate == null)
                    return BadRequest("Booking cannot be null");
                if (bookingToCreate.UsingFrom < DateTime.Now)
                    return BadRequest("You can't create a booking for a date that has passed");
                if (bookingToCreate.UsingFrom >= bookingToCreate.UsingTill)
                    return BadRequest("Time from must be behind/be the same as time to");
                if (await _lineUpRepository.EntityExists(bookingToCreate))
                    return BadRequest("Booking already exists");
                bookingToCreate.Status = BookingStatus.Reserved;
                bookingToCreate.NoOfGuests = bookingDTO.NoOfGuests;
                bookingToCreate.BookingTime = DateTime.Now;
                bookingToCreate.TotalPrice = bookingDTO.TotalPrice;
                bookingToCreate.IdOfSpaceBooked = bookingDTO.IdOfSpaceBooked;
                bookingToCreate.BookedById = bookingDTO.BookedById;
                bookingToCreate.AmenitiesSelected = bookingFromClientDTO.AmenitiesSelected;
                bookingToCreate.CreatedByOwner = bookingDTO.CreatedByOwner;
                timeOfCreation = bookingToCreate.BookingTime;
                bookingToCreate.BookingRef = "BKN" + bookingToCreate.BookedById.ToString() + bookingToCreate.IdOfSpaceBooked.ToString() + Guid.NewGuid().ToString().GetHashCode().ToString("x");
                user.Bookings.Add(bookingToCreate);
            }
            await _lineUpRepository.SaveAllChanges();
            BookingQuery query = new BookingQuery {
                TimeBooked = timeOfCreation.Date
            };
            // var user = await _userRepository.GetUser(bookingToCreate.BookedById);
            var bookingDetails = await _lineUpRepository.GetBookingDetails(bookingFromClientDTO.BookedById, query);
            Message message = new Message();
            message.Subject = "Booking";
            message.FromEmail = "noreply@234spaces.com";
            message.FromName = "234Spaces Admin";
            message.ToName = user.Name;
            message.ToEmail = user.Email;
            message.PlainContent = null;
            if(bookingDetails.Count() > 1) {
                var reservationIdList = String.Empty;
                var totalPrice = 0.00;
                foreach (var item in bookingDetails)
                {
                    reservationIdList += item.BookingRef.ToString() + ", ";
                    totalPrice  += item.TotalPrice;
                }
                message.HtmlContent = FormattedEmailBody2("Your Reservations", $"The following reservations has been placed for you, your reservation references are {reservationIdList.ToString()}, kindly pay #{totalPrice.ToString()} to confirm the reservation", "", "", false);
            } else {
                message.HtmlContent = FormattedEmailBody2("Your Reservation", $"A reservation has been placed for you, your reservation reference is {bookingDetails[0].BookingRef.ToString()}, kindly pay #{bookingDetails[0].TotalPrice.ToString()} to confirm the reservation", "", "", false);
            }
            _userRepository.EmailSender(message);

            return Ok();
        }
        [HttpGet("getBookings/{userId}")]
        public async Task<QueryResult<Booking>> GetBookings(int userId, [FromQuery] BookingQueryDTO queryDTO)
        {   
            var query = _mapper.Map<BookingQuery>(queryDTO);
            return await _lineUpRepository.GetBookings(userId, query);
        }

        [HttpGet("getCustomerBookings/{userId}")]
        public async Task<QueryResult<CustomerBookingsToReturn>> GetCustomerBookings(int userId, [FromQuery] BookingQueryDTO queryDTO)
        {
            var query = _mapper.Map<BookingQuery>(queryDTO);
            var customerBookings = await _lineUpRepository.GetCustomerBookings(userId, query);
            return customerBookings;
        }

        [HttpGet("getMerchantBookings/{userId}")]
        public async Task<IActionResult> GetMerchantBookings(int userId, [FromQuery] BookingQueryDTO queryDTO)
        {
            // if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value) || (User.FindFirst(ClaimTypes.Role).Value != Role.AnySpaces.ToString()) || (User.FindFirst(ClaimTypes.Role).Value != Role.Merchant.ToString())) {
            //     return Unauthorized();
            // }
            // var query = _mapper.Map<BookingQuery>(queryDTO);
            var dateStart = DateTime.Parse(queryDTO.DateStart);
            var dateEnd = DateTime.Parse(queryDTO.DateEnd);
            var query = new BookingQuery
            {
                SortBy = queryDTO.SortBy,
                SearchString = queryDTO.SearchString,
                IsSortAscending = queryDTO.IsSortAscending,
                Page = queryDTO.Page,
                Status = queryDTO.Status,
                PageSize = queryDTO.PageSize,
                CurrentPage = queryDTO.CurrentPage,
                DateStart = dateStart,
                DateEnd = dateEnd
            };
            return Ok(await _lineUpRepository.GetBookings(userId, query));
        }

        [HttpGet("getMerchantReservations/{userId}")]
        public async Task<IActionResult> GetMerchantReservations(int userId, [FromQuery] BookingQueryDTO queryDTO)
        {
            // if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value) || (User.FindFirst(ClaimTypes.Role).Value != Role.AnySpaces.ToString()) || (User.FindFirst(ClaimTypes.Role).Value != Role.Merchant.ToString())) {
            //     return Unauthorized();
            // }
            var query = _mapper.Map<BookingQuery>(queryDTO);
            return Ok(await _lineUpRepository.GetBookings(userId, query));
        }

        [HttpPost("updateBooking/{bookingId}")]
        public async Task<IActionResult> UpdateBooking(int bookingId, [FromBody] BookingDTO bookingDTO)
        {
            var booking = await _lineUpRepository.GetBooking(bookingId);
            var bookingToUpdate = _mapper.Map<BookingDTO, Booking>(bookingDTO, booking);
            if (bookingToUpdate == null)
                return BadRequest("Booking cannot be null");
            var bookingExists = await _lineUpRepository.EntityExists(bookingToUpdate);
            if (!bookingExists)
                return BadRequest("Booking does not exist");
            _lineUpRepository.Update(bookingToUpdate);
            await _lineUpRepository.SaveAllChanges();
            return Ok();
        }

        [HttpPost("createEnquiry")]
        public async Task<IActionResult> CreateEnquiry([FromBody] EnquiryDTO enquiryDTO)
        {
            var enquiryToCreate = _mapper.Map<Enquiry>(enquiryDTO);
            if (enquiryToCreate == null)
                return BadRequest("Enuiry cannot be null");
            if (await _lineUpRepository.EntityExists(enquiryToCreate))
                return BadRequest("Enquiry already exists");
            _lineUpRepository.Add(enquiryToCreate);
            await _lineUpRepository.SaveAllChanges();
            return Ok();
        }

        [HttpDelete("deleteEnquiry/{enquiryId}")]
        public IActionResult DeleteEnquiry(int enquiryId)
        {
            var enquiryToDispose = _lineUpRepository.GetEnquiry(enquiryId);
            if (enquiryToDispose != null)
            {
                _lineUpRepository.Delete(enquiryToDispose);
                _lineUpRepository.SaveAllChanges();
                return Ok();
            }
            return NotFound();
        }

        [HttpPut("addMessageToBooking/{spaceId}")]
        public async Task<IActionResult> MessageSpaceOwner(int bookingId, [FromBody] ChatMessageDTO chatMessageDTO)
        {
            var booking = await _lineUpRepository.GetBooking(bookingId);
            if (booking == null)
                return BadRequest("The booking you are trying to access does not exist");
            var chatMessage = _mapper.Map<ChatMessage>(chatMessageDTO);
            chatMessage.Time = DateTime.Now;
            booking.Chat.ChatMessages.Add(chatMessage);
            await _lineUpRepository.SaveAllChanges();
            return Ok();
        }

        [HttpPost("acceptBooking/{bookingId}")]
        public async Task<IActionResult> AcceptBooking(int bookingId)
        {
            // if ((User.FindFirst(ClaimTypes.Role).Value != Role.AnySpaces.ToString()) || (User.FindFirst(ClaimTypes.Role).Value != Role.Merchant.ToString())) {
            //     return Unauthorized();
            // }
            var booking = await _lineUpRepository.GetBooking(bookingId);
            if (booking == null)
                return BadRequest("Booking not found");
            booking.Status = BookingStatus.Booked;
            await _lineUpRepository.SaveAllChanges();
            return Ok();
        }

        [HttpGet("getMerchants")]
        public async Task<IActionResult> GetMerchants()
        {
            // if (User.FindFirst(ClaimTypes.Role).Value != Role.AnySpaces.ToString()) {
            //     return Unauthorized();
            // }
            var merchants = await _lineUpRepository.GetMerchants();
            var queryResultToReturn = new QueryResult<UserToReturnDTO>();
            queryResultToReturn.TotalItems = merchants.TotalItems;
            var merchantsToReturn = _mapper.Map<IEnumerable<User>, IEnumerable<UserToReturnDTO>>(merchants.Items);
            queryResultToReturn.Items = merchantsToReturn;
            return Ok(queryResultToReturn);
        }

        private string FormattedEmailBody(string heading, string body, string buttonLink, string buttonName, bool hasButton)
        {
            string first = "<!DOCTYPE html> <html lang='en'> <head><meta charset='UTF-8'><meta name='viewport' content='width=device-width, initial-scale=1.0'><meta http-equiv='X-UA-Compatible' content='ie=edge'><title>234Spaces</title><link rel='stylesheet' href='https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css'><link rel='stylesheet' href='https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/css/bootstrap.min.css'></head><body><style>*,*::before { padding: 0; box-sizing: inherit;}html {box-sizing: border-box;}@media only screen and (max-width: 37.5em) { html {font-size: 62.5%; }}@media only screen and (max-width: 25em) {html { font-size: 50%;}}body {font-family: 'Nunito', sans-serif;color: #6D5D4B;font-weight: 300; outline: none; line-height: 1.6;   max-width: 100%;}.content {height: 100vh;}@media only screen and (max-width: 31.25em) {.header {padding: .2rem !important;}.header-link {padding: 0 1rem 0 1rem !important;}.booking {text-align: center !important;}}.header-link, .header-link:hover {color: #EAEAEA;font-size: 1.5rem;text-decoration: none;}.booking {font-size: 1rem;text-transform: uppercase;}.sub-head {padding: 1.5rem 0 1.5rem 0;border-bottom: 1px solid #aaa;margin-bottom: .5rem;}.logo{height: 25%;width: 25%;}.btn-2 {padding: .375rem .75rem !important;}.footer {line-height: .5; margin-bottom: 1.5rem;font-size: .8rem;}@media only screen and (max-width: 25em) { .footer { font-size: 1rem; }.footer-icon {height: 2rem !important;width: 2rem !important;padding: 2.22px !important;font-size: 1.3rem !important;}.footer-icon {height: 2.5rem;width: 2.5rem;padding: 6px;font-size: 1.465rem;vertical-align: middle;text-align: center;border-radius: 50%;background-color: #2B2C2E;color: #EBEBEB;}</style><div class='content'><div class='header'><a href='#' class='px-5 header-link'><img class='logo' src='https://res.cloudinary.com/dro0fy3gz/image/upload/v1576489895/Logo/234Spaces_logo_PNG_2_qxc8rn.png'></a></div><div class='px-3'><h3 class='pt-1 pb-1 text-center'>" + heading + "</h3><div class='sub-head'><div class='d-flex justify-content-center align-item-center'><div class='lead'><p>" + body + "</p></div></div></div>";
            string second = hasButton ? "<div class='sub-head'><div class='d-flex justify-content-center align-item-center'><div><a href='" + buttonLink + "' class='btn btn-primary btn-lg'>" + buttonName + "</a></div></div></div>" : "";
            string third = "</div><div class='jumbotron p-3'><div class='d-flex justify-content-end mb-4 mt-5'><a href=''><i class='fa fa-facebook mx-2 footer-icon'></i></a><a href=''><i class='fa fa-twitter mx-2 footer-icon'></i></a><a href=''><i class='fa fa-linkedin mx-2 footer-icon'></i></a> </div><div class='footer'><p>234Spaces Inc., 2019. All rights reserved.</p><p>229 West 43rd Street 5th Floor New York, NY 10036</p></div></div></div></body></html>";

            return first + second + third;
        }
        private string FormattedEmailBody2(string heading, string body, string buttonLink, string buttonName, bool hasButton)
        {
            string first = "<!DOCTYPE html><html lang='en' dir='ltr'><head><meta charset='utf-8'><meta name='viewport' content='width=device-width'><style type='text/css'>@media screen and (max-width: 525px){table[class='wrapper']{width: 100% !important;}td[class='logo']{text-align: left; padding: 20px 0 20px 0 !important;}td[class='logo'] img{margin: 0 auto!important;}td[class='mobile-hide']{display: none;}img[class='mobile-hide']{display: none !important;}img[class='img-max']{max-width: 100% !important; height: auto !important;}table[class='responsive-table']{width: 100%!important;}td[class='padding']{padding: 10px 5% 15px 5% !important;}td[class='padding-copy']{padding: 10px 5% 10px 5% !important; text-align: center;}td[class='padding-meta']{padding: 30px 5% 0px 5% !important; text-align: center;}td[class='no-pad']{padding: 0 0 20px 0 !important;}td[class='no-padding']{padding: 0 !important;}td[class='section-padding']{padding: 50px 15px 50px 15px !important;}td[class='section-padding-bottom-image']{padding: 50px 15px 0 15px !important;}td[class='mobile-wrapper']{padding: 10px 5% 15px 5% !important;}table[class='mobile-button-container']{margin: 0 auto; width: 100% !important;}a[class='mobile-button']{width: 80% !important; padding: 15px !important; border: 0 !important; font-size: 16px !important;}}</style></head><body style='-webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%; max-width: 575px; margin: 0 auto; padding: 0; height: 100%; width: 100%;' dir='ltr'><table border='0' cellpadding='0' cellspacing='0' width='100%' style='-webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%; mso-table-lspace: 0pt; mso-table-rspace: 0pt; border-collapse: collapse;'> <tr> <td bgcolor='#ffffff' style='-webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%; mso-table-lspace: 0pt; mso-table-rspace: 0pt;'> <div align='center' style='padding: 0px 15px 0px 15px;'> <table border='0' cellpadding='0' cellspacing='0' width='500' class='wrapper' style='-webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%; mso-table-lspace: 0pt; mso-table-rspace: 0pt; border-collapse: collapse;'> <tr> <td style='-webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%; mso-table-lspace: 0pt; mso-table-rspace: 0pt; padding: 30px 0px 30px 0px;' class='logo'> <table border='0' cellpadding='0' cellspacing='0' width='100%' style='-webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%; mso-table-lspace: 0pt; mso-table-rspace: 0pt; border-collapse: collapse;'> <tr> <td bgcolor='#ffffff' width='400' align='center' class='mobile-hide' style='-webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%; mso-table-lspace: 0pt; mso-table-rspace: 0pt;'> <table border='0' cellpadding='0' cellspacing='0' style='-webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%; mso-table-lspace: 0pt; mso-table-rspace: 0pt; border-collapse: collapse;'> <tr> <td align='center' style='-webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%; mso-table-lspace: 0pt; mso-table-rspace: 0pt; padding: 0 0 5px 0; font-size: 23px; font-family: 'Helvetica Neue', Helvetica, Arial, sans-serif; color: #666666; text-decoration: none;'> <span class='branding' style='color: #666666; text-decoration: none;'> <a href='http://234Spaces.com' target='_blank' style='-webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%; color: inherit; text-decoration: none;'> <img class='logo' style='height: auto; width: 200px;' src='https://res.cloudinary.com/dro0fy3gz/image/upload/v1576489895/Logo/234Spaces_logo_PNG_2_qxc8rn.png'></a></span> </td></tr></table> </td></tr></table> </td></tr></table> </div></td></tr></table><table border='0' cellpadding='0' cellspacing='0' width='100%' style='-webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%; mso-table-lspace: 0pt; mso-table-rspace: 0pt; border-collapse: collapse;'> <tr> <td bgcolor='#f8f8f8' align='center' style='-webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%; mso-table-lspace: 0pt; mso-table-rspace: 0pt; padding: 70px 15px 70px 15px;' class='section-padding-bottom-image'> <table border='0' cellpadding='0' cellspacing='0' width='500' class='responsive-table' style='-webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%; mso-table-lspace: 0pt; mso-table-rspace: 0pt; border-collapse: collapse;'> <tr> <td style='-webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%; mso-table-lspace: 0pt; mso-table-rspace: 0pt;'> <table width='100%' border='0' cellspacing='0' cellpadding='0' style='-webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%; mso-table-lspace: 0pt; mso-table-rspace: 0pt; border-collapse: collapse;'> <tr> <td style='-webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%; mso-table-lspace: 0pt; mso-table-rspace: 0pt;'> <table width='100%' border='0' cellspacing='0' cellpadding='0' style='-webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%; mso-table-lspace: 0pt; mso-table-rspace: 0pt; border-collapse: collapse;'> <tr> <td align='center' style='-webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%; mso-table-lspace: 0pt; mso-table-rspace: 0pt; font-size: 25px; font-family: 'Helvetica Neue', Helvetica, Arial, sans-serif; color: #333333;' class='padding-copy'>" + heading + "</td></tr><tr> <td align='center' style='-webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%; mso-table-lspace: 0pt; mso-table-rspace: 0pt; padding: 20px 0 0 0; font-size: 16px; line-height: 25px; font-family: 'Helvetica Neue', Helvetica, Arial, sans-serif; color: #666666;' class='padding-copy'><p>" + body + "</p></td></tr></table> </td></tr><tr> <td style='-webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%; mso-table-lspace: 0pt; mso-table-rspace: 0pt;'> </td></tr></table> </td></tr></table> </td></tr></table><table border='0' cellpadding='0' cellspacing='0' width='100%' style='-webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%; mso-table-lspace: 0pt; mso-table-rspace: 0pt; border-collapse: collapse;'> <tr> <td bgcolor='#ffffff' align='center' style='-webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%; mso-table-lspace: 0pt; mso-table-rspace: 0pt; padding: 15px 15px 40px 15px;' class='section-padding'> <table border='0' cellpadding='0' cellspacing='0' width='500' class='responsive-table' style='-webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%; mso-table-lspace: 0pt; mso-table-rspace: 0pt; border-collapse: collapse;'> <tr> <td style='-webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%; mso-table-lspace: 0pt; mso-table-rspace: 0pt;'> <table width='100%' border='0' cellspacing='0' cellpadding='0' style='-webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%; mso-table-lspace: 0pt; mso-table-rspace: 0pt; border-collapse: collapse;'> <tr> <td style='-webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%; mso-table-lspace: 0pt; mso-table-rspace: 0pt;'> <table align='center' width='100%' border='0' cellspacing='0' cellpadding='0' style='-webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%; mso-table-lspace: 0pt; mso-table-rspace: 0pt; border-collapse: collapse;'> <tr> <td align='center' style='-webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%; mso-table-lspace: 0pt; mso-table-rspace: 0pt; padding: 25px 0 0 0; font-size: 16px; line-height: 25px; font-family: 'Helvetica Neue', Helvetica, Arial, sans-serif; color: #666666;' class='padding-copy'>";

            string second = hasButton ? "<p>" + "Please click the button below to verify your account" + "</p></td></tr></table> </td></tr><tr> <td style='-webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%; mso-table-lspace: 0pt; mso-table-rspace: 0pt;'> <table align='center' width='100%' border='0' cellspacing='0' cellpadding='0' class='mobile-button-container' style='-webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%; mso-table-lspace: 0pt; mso-table-rspace: 0pt; border-collapse: collapse;'> <tr> <td align='center' style='-webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%; mso-table-lspace: 0pt; mso-table-rspace: 0pt; padding: 25px 0 0 0;' class='padding-copy'> <table border='0' cellspacing='0' cellpadding='0' class='responsive-table' style='-webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%; mso-table-lspace: 0pt; mso-table-rspace: 0pt; border-collapse: collapse;'> <tr> <td align='center' style='-webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%; mso-table-lspace: 0pt; mso-table-rspace: 0pt;'><a href='" + buttonLink + "target='_blank' style='-webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%; font-size: 16px; font-family: 'Helvetica Neue', Helvetica, Arial, sans-serif; font-weight: normal; color: #ffffff; text-decoration: none; background-color: #33b5e5; border-top: 15px solid #33b5e5; border-bottom: 15px solid #33b5e5; border-left: 25px solid #33b5e5; border-right: 25px solid #33b5e5; border-radius: 3px; -webkit-border-radius: 3px; -moz-border-radius: 3px; display: inline-block;' class='mobile-button'>" + buttonName + "&rarr;</a></td></tr></table> </td></tr></table> </td></tr></table> </td></tr></table> </td></tr>" : "";

            string third = "<tr> <td bgcolor='#f8f8f8' align='center' style='-webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%; mso-table-lspace: 0pt; mso-table-rspace: 0pt;'> <table width='100%' border='0' cellspacing='0' cellpadding='0' align='center' style='-webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%; mso-table-lspace: 0pt; mso-table-rspace: 0pt; border-collapse: collapse;'> <tr> <td style='-webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%; mso-table-lspace: 0pt; mso-table-rspace: 0pt; padding: 20px 0px 20px 0px;'> <table width='500' border='0' cellspacing='0' cellpadding='0' align='center' class='responsive-table' style='-webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%; mso-table-lspace: 0pt; mso-table-rspace: 0pt; border-collapse: collapse;'> <tr> <td align='center' valign='middle' style='-webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%; mso-table-lspace: 0pt; mso-table-rspace: 0pt; font-size: 12px; line-height: 18px; font-family: 'Helvetica Neue', Helvetica, Arial, sans-serif; color: #666666;'> <span class='appleFooter' style='color:#666666;'> &copy;" + DateTime.Now.Year.ToString() + "<a href='http://234Spaces.com' target='_blank' style='-webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%; color: #999999; text-decoration: none;'>NETSAFARI</a>. All rights reserved. </span> <br></td></tr></table> </td></tr></table> </td></tr></table></body></html>";

            return first + second + third;
        }

        // private string NewEmailBody(string heading, string body, string buttonLink, string buttonName, bool hasButton)
        // {
        //     string first = ""
        // }

        [HttpGet("getMetrics/{userId}")]
        public async Task<IActionResult> GetMerchantMetrics(int userId)
        {
            // if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value) || (User.FindFirst(ClaimTypes.Role).Value != Role.AnySpaces.ToString()) || (User.FindFirst(ClaimTypes.Role).Value != Role.Merchant.ToString())) {
            //     return Unauthorized();
            // }
            var metrics = await _lineUpRepository.GetMerchantMetrics(userId);
            return Ok(metrics);
        }
    }
}