using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Controllers.DTOs;
using API.Core;
using API.Core.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/lineUp")]
    [ApiController]
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

        [HttpPost("createSpace")]
        public async Task<IActionResult> CreateSpace([FromBody] SpaceDTO spaceDTO)
        {
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
        [HttpGet("getSpace/{spaceId}")]
        public async Task<IActionResult> GetSpace(int spaceId)
        {
            var space = await _lineUpRepository.GetSpace(spaceId);
            if (space == null)
                return NotFound("Space does not exist");
            return Ok(_mapper.Map<SpaceDTO>(space));
        }
        [HttpPut("updateSpace/{spaceId}")]
        public async Task<IActionResult> UpdateSpace(int spaceId, [FromBody] SpaceDTO spaceDTO)
        {
            var space = await _lineUpRepository.GetSpace(spaceId);
            // _lineUpRepository.Delete(space.Amenities);
            var spaceToUpdate = _mapper.Map<SpaceDTO, Space>(spaceDTO, space);
            if (spaceToUpdate == null)
                return BadRequest("Space cannot be null");
            if (await _lineUpRepository.EntityExists(spaceToUpdate))
            {
                // spaceToUpdate.Amenities.Entr
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
        public async Task<QueryResult<Space>> GetMerchantSpaces([FromQuery] SpaceQueryDTO queryDTO)
        {
            var query = _mapper.Map<SpaceQuery>(queryDTO);
            var spacesQueryResult = await _lineUpRepository.GetMerchantSpaces(query);
            return spacesQueryResult;
        }

        [HttpGet("getSpaceTypes")]
        public async Task<IEnumerable<SpaceType>> GetSpaceTypes()
        {
            var spaceTypes = await _lineUpRepository.GetSpaceTypes();
            return spaceTypes;
        }
        [HttpPost("createSpaceType")]
        public async Task<IActionResult> CreateSpaceType([FromBody] SpaceTypeDTO spaceTypeDTO)
        {
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
            var amenityToCreate = _mapper.Map<Amenity>(amenityDTO);
            if (amenityToCreate == null)
                return BadRequest("Amenity cannot be null");
            if (await _lineUpRepository.EntityExists(amenityToCreate))
                return BadRequest("Amenity already exists");
            _lineUpRepository.Add(amenityToCreate);
            await _lineUpRepository.SaveAllChanges();
            return Ok();
        }

        [HttpPut("updateAmenity/{amenityId}")]
        public async Task<IActionResult> UpdateAmenity(int amenityId, [FromBody] AmenityDTO amenityDTO)
        {
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
            var existingBookingTimes = await _lineUpRepository.GetBookedTimes(bookingToCreate.SpaceBooked.Id, bookingToCreateBT);
            if (existingBookingTimes.Count() > 0)
                return BadRequest("You can't select from a range of booking that already exists");
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
                SpaceBooked = bookingFromClientDTO.SpaceBooked,
                UserId = bookingFromClientDTO.UserId,
                BookingTime = bookingFromClientDTO.BookingTime,
                TotalPrice = bookingFromClientDTO.TotalPrice,
                Chat = bookingFromClientDTO.Chat,
                BookedById = bookingFromClientDTO.BookedById,
                Status = bookingFromClientDTO.Status
            };
            var bookingToCreate = new Booking();
            foreach (var time in bookingFromClientDTO.UsingTimes)
            {
                bookingToCreate.UsingFrom = time.UsingFrom;
                bookingToCreate.UsingTill = time.UsingTill;
                var bookingToCreateBT = new BookedTimes();
                bookingToCreateBT.From = bookingToCreate.UsingFrom;
                bookingToCreateBT.To = bookingToCreate.UsingTill;
                var existingBookingTimes = await _lineUpRepository.GetBookedTimes(bookingToCreate.SpaceBooked.Id, bookingToCreateBT);
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
                    reservationIdList += item.Id.ToString() + " ,";
                    totalPrice  += item.TotalPrice;
                }
                message.HtmlContent = FormattedEmailBody("Your Booking", $"The following bookings has been placed for you, your booking references are {reservationIdList.ToString()}.", "", "", false);
            } else {
                message.HtmlContent = FormattedEmailBody("Your Booking", $"Your booking has been confirmed, your bookingReference is {bookingDetails[0].Id.ToString()}", "", "", false);
            }
            _userRepository.EmailSender(message);

            return Ok();
        }
        [HttpPost("createReservation")]
        public async Task<IActionResult> CreateReservation([FromBody] BookingFromClientDTO bookingFromClientDTO)
        {
            var bookingDTO = new BookingDTO {
                SpaceBooked = bookingFromClientDTO.SpaceBooked,
                UserId = bookingFromClientDTO.UserId,
                BookingTime = bookingFromClientDTO.BookingTime,
                TotalPrice = bookingFromClientDTO.TotalPrice,
                Chat = bookingFromClientDTO.Chat,
                BookedById = bookingFromClientDTO.BookedById,
                Status = bookingFromClientDTO.Status
            };
            var bookingToCreate = new Booking();
            foreach (var time in bookingFromClientDTO.UsingTimes)
            {
                bookingToCreate.UsingFrom = time.UsingFrom;
                bookingToCreate.UsingTill = time.UsingTill;
                var bookingToCreateBT = new BookedTimes();
                bookingToCreateBT.From = bookingToCreate.UsingFrom;
                bookingToCreateBT.To = bookingToCreate.UsingTill;
                var existingBookingTimes = await _lineUpRepository.GetBookedTimes(bookingToCreate.SpaceBooked.Id, bookingToCreateBT);
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
        [HttpGet("getBookings/{userId}")]
        public async Task<QueryResult<Booking>> GetBookings(int userId, [FromQuery] BookingQueryDTO queryDTO)
        {
            var query = _mapper.Map<BookingQuery>(queryDTO);
            return await _lineUpRepository.GetBookings(userId, query);
        }

        [HttpGet("getCustomerBookings/{userId}")]
        public async Task<QueryResult<Booking>> GetCustomerBookings(int userId, [FromQuery] BookingQueryDTO queryDTO)
        {
            var query = _mapper.Map<BookingQuery>(queryDTO);
            return await _lineUpRepository.GetCustomerBookings(userId, query);
        }

        [HttpGet("getMerchantBookings/{userId}")]
        public async Task<QueryResult<Booking>> GetMerchantBookings(int userId, [FromQuery] BookingQueryDTO queryDTO)
        {
            // var query = _mapper.Map<BookingQuery>(queryDTO);
            var dateStart = DateTime.Parse(queryDTO.DateStart);
            var dateEnd = DateTime.Parse(queryDTO.DateEnd);
            var query = new BookingQuery
            {
                SortBy = queryDTO.SortBy,
                SearchString = queryDTO.SearchString,
                IsSortAscending = queryDTO.IsSortAscending,
                Page = queryDTO.Page,
                PageSize = queryDTO.PageSize,
                CurrentPage = queryDTO.CurrentPage,
                DateStart = dateStart,
                DateEnd = dateEnd
            };
            return await _lineUpRepository.GetBookings(userId, query);
        }

        [HttpGet("getMerchantReservations/{userId}")]
        public async Task<QueryResult<Booking>> GetMerchantReservations(int userId, [FromQuery] BookingQueryDTO queryDTO)
        {
            var query = _mapper.Map<BookingQuery>(queryDTO);
            return await _lineUpRepository.GetBookings(userId, query);
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
            string third = "</div><div class='jumbotron p-3'><div class='d-flex justify-content-end mb-4 mt-5'><a href=''><i class='fa fa-facebook mx-2 footer-icon'></i></a><a href=''><i class='fa fa-twitter mx-2 footer-icon'></i></a><a href=''><i class='fa fa-linkedin mx-2 footer-icon'></i></a> </div><div class='footer'><p>234Spaces Inc., 2019. All rights reserved.</p><p>229 West 43rd Street 5th Floor New York, NY 10036</p></div></div></div></body></html>}}}";

            return first + second + third;
        }

        // [HttpGet("getMetrics")]
        // public async Task<IActionResult> GetMerchantMetrics()
        // {

        // }
    }
}