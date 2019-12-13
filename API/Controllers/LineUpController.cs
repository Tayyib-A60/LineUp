using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Controllers.DTOs;
using API.Core;
using API.Core.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers {
    [Route("api/lineUp")]
    [ApiController]
    public class LineUpController : ControllerBase {
        public IMapper _mapper { get; }
        public ILineUpRepository _lineUpRepository { get; }
        public LineUpController (ILineUpRepository lineUpRepository, IMapper mapper)
        {
            _lineUpRepository = lineUpRepository;
            _mapper = mapper;
        }
        [HttpPost("createSpace")]
        public async Task<IActionResult> CreateSpace([FromBody] SpaceDTO spaceDTO)
        {
            var spaceToCreate = _mapper.Map<Space>(spaceDTO);
            if(spaceToCreate == null)
                return BadRequest("Space cannot be null");
            if(await _lineUpRepository.EntityExists(spaceToCreate))
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
            if(space == null)
                return NotFound("Space does not exist");
            return Ok(_mapper.Map<SpaceDTO>(space));
        }
        [HttpPut("updateSpace/{spaceId}")]
        public async Task<IActionResult> UpdateSpace(int spaceId, [FromBody] SpaceDTO spaceDTO)
        {
            var space = await _lineUpRepository.GetSpace(spaceId);
            // _lineUpRepository.Delete(space.Amenities);
            var spaceToUpdate = _mapper.Map<SpaceDTO, Space>(spaceDTO,space);
            if(spaceToUpdate == null)
                return BadRequest("Space cannot be null");
            if(await _lineUpRepository.EntityExists(spaceToUpdate)) {
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
            if(spaceToDispose != null) {
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
            if(spaceTypeToCreate == null)
                return BadRequest("SpaceType cannot be null");
            if(await _lineUpRepository.EntityExists(spaceTypeToCreate))
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
            if(spaceTypeToUpdate == null)
                return BadRequest("Spacetype cannot be null");
            if(await _lineUpRepository.EntityExists(spaceTypeToUpdate) == false)
                return BadRequest("Spacetype does not exist");
            _lineUpRepository.Update(spaceTypeToUpdate);
            await _lineUpRepository.SaveAllChanges();
            return Ok();
        }

        [HttpDelete("deleteSpaceType/{spaceTypeId}")]
        public IActionResult DeleteSpaceType(int spaceTypeId)
        {
            var spaceTypeToDispose = _lineUpRepository.GetSpaceType(spaceTypeId);
            if(spaceTypeToDispose != null) {
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
            if(amenityToCreate == null)
                return BadRequest("Amenity cannot be null");
            if(await _lineUpRepository.EntityExists(amenityToCreate))
                return BadRequest("Amenity already exists");
            _lineUpRepository.Add(amenityToCreate);
            await _lineUpRepository.SaveAllChanges();
            return Ok();
        }

        [HttpPut("updateAmenity/{amenityId}")]
        public async Task<IActionResult> UpdateAmenity(int amenityId,[FromBody] AmenityDTO amenityDTO)
        {
            var amenity = await _lineUpRepository.GetAmenity(amenityId);
            var amenityToUpdate = _mapper.Map<AmenityDTO,Amenity>(amenityDTO, amenity);
            if(amenityToUpdate == null)
                return BadRequest("Amenity cannot be null");
            if(await _lineUpRepository.EntityExists(amenityToUpdate) == false)
                return BadRequest("Amenity does not exist");
            _lineUpRepository.Update(amenityToUpdate);
            return Ok();
        }

        [HttpPost("createReservation")]
        public async Task<IActionResult> CreateReservation([FromBody] BookingDTO bookingDTO)
        {
            var bookingToCreate = _mapper.Map<Booking>(bookingDTO);
            if(bookingToCreate == null)
                return BadRequest("Booking cannot be null");
            if(bookingToCreate.UsingFrom < DateTime.Now)
                return BadRequest("You can't create a booking for a date that has passed");
            if(bookingToCreate.UsingFrom >= bookingToCreate.UsingTill)
                return BadRequest("Time from must be behind/be the same as time to");
            if(await _lineUpRepository.EntityExists(bookingToCreate))
                return BadRequest("Booking already exists");
            var bookingToCreateBT = new BookedTimes();
            bookingToCreateBT.From = bookingToCreate.UsingFrom;
            bookingToCreateBT.To = bookingToCreate.UsingTill;
            var existingBookingTimes = await _lineUpRepository.GetBookedTimes(bookingToCreate.SpaceBooked.Id, bookingToCreateBT);
            if(existingBookingTimes.Count() > 0)
                return BadRequest("You can't select from a range of booking that already exists");
            bookingToCreate.Status = BookingStatus.Reserved;
            bookingToCreate.BookingTime = DateTime.Now;
            _lineUpRepository.Add(bookingToCreate);
            await _lineUpRepository.SaveAllChanges();
            return Ok();
        }
        [HttpPost("createBooking")]
        public async Task<IActionResult> CreateBooking([FromBody] BookingDTO bookingDTO)
        {
            var bookingToCreate = _mapper.Map<Booking>(bookingDTO);
            var bookingToCreateBT = new BookedTimes();
            bookingToCreateBT.From = bookingToCreate.UsingFrom;
            bookingToCreateBT.To = bookingToCreate.UsingTill;
            var existingBookingTimes = await _lineUpRepository.GetBookedTimes(bookingToCreate.SpaceBooked.Id, bookingToCreateBT);
            if(existingBookingTimes.Count() > 0)
                return BadRequest("You can't select from a range of booking that already exists");
            if(bookingToCreate == null)
                return BadRequest("Booking cannot be null");
            if(bookingToCreate.UsingFrom < DateTime.Now)
                return BadRequest("You can't create a booking for a date that has passed");
            if(bookingToCreate.UsingFrom >= bookingToCreate.UsingTill)
                return BadRequest("Time from must be behind/be the same as time to");
            if(await _lineUpRepository.EntityExists(bookingToCreate))
                return BadRequest("Booking already exists");
            bookingToCreate.Status = BookingStatus.Booked;
            bookingToCreate.BookingTime = DateTime.Now;
            _lineUpRepository.Add(bookingToCreate);
            await _lineUpRepository.SaveAllChanges();
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
            var query = new BookingQuery{
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
        public async Task<IActionResult> UpdateBooking(int bookingId,[FromBody] BookingDTO bookingDTO)
        {
            var booking = await _lineUpRepository.GetBooking(bookingId);
            var bookingToUpdate = _mapper.Map<BookingDTO, Booking>(bookingDTO, booking);
            if(bookingToUpdate == null)
                return BadRequest("Booking cannot be null");
            var bookingExists = await _lineUpRepository.EntityExists(bookingToUpdate);
            if(!bookingExists)
                return BadRequest("Booking does not exist");
            _lineUpRepository.Update(bookingToUpdate);
            await _lineUpRepository.SaveAllChanges();
            return Ok();
        }

        [HttpPost("createEnquiry")]
        public async Task<IActionResult> CreateEnquiry([FromBody] EnquiryDTO enquiryDTO)
        {
            var enquiryToCreate = _mapper.Map<Enquiry>(enquiryDTO);
            if(enquiryToCreate == null)
                return BadRequest("Enuiry cannot be null");
            if(await _lineUpRepository.EntityExists(enquiryToCreate))
                return BadRequest("Enquiry already exists");
            _lineUpRepository.Add(enquiryToCreate);
            await _lineUpRepository.SaveAllChanges();
            return Ok();
        }

        [HttpDelete("deleteEnquiry/{enquiryId}")]
        public IActionResult DeleteEnquiry(int enquiryId)
        {
            var enquiryToDispose = _lineUpRepository.GetEnquiry(enquiryId);
            if(enquiryToDispose != null) {
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
            if(booking == null)
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
            if(booking == null)
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
    }
}