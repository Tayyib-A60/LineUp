using System;
using System.Collections.Generic;
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
            var spaceToUpdate = _mapper.Map<SpaceDTO, Space>(spaceDTO,space);
            if(spaceToUpdate == null)
                return BadRequest("Space cannot be null");
            if(await _lineUpRepository.EntityExists(spaceToUpdate)) {
                _lineUpRepository.Update(spaceToUpdate);
                await _lineUpRepository.SaveAllChanges();
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("deleteSpace/{spaceId}")]
        public IActionResult DeleteSpace(int spaceId)
        {
            var spaceToDispose = _lineUpRepository.GetSpace(spaceId);
            if(spaceToDispose != null) {
                _lineUpRepository.Delete(spaceToDispose);
                _lineUpRepository.SaveAllChanges();
                return Ok();
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

        [HttpPost("createBooking")]
        public async Task<IActionResult> CreateBooking([FromBody] BookingDTO bookingDTO)
        {
            var bookingToCreate = _mapper.Map<Booking>(bookingDTO);
            if(bookingToCreate == null)
                return BadRequest("Booking cannot be null");
            if(await _lineUpRepository.EntityExists(bookingToCreate))
                return BadRequest("Booking already exists");
            bookingToCreate.Status = BookingStatus.Reserved;
            _lineUpRepository.Add(bookingToCreate);
            await _lineUpRepository.SaveAllChanges();
            return Ok();
        }

        [HttpPost("updateBooking/{bookingId}")]
        public async Task<IActionResult> UpdateBooking(int bookingId,[FromBody] BookingDTO bookingDTO)
        {
            var booking = await _lineUpRepository.GetBooking(bookingId);
            var bookingToUpdate = _mapper.Map<BookingDTO, Booking>(bookingDTO, booking);
            if(bookingToUpdate == null)
                return BadRequest("Booking cannot be null");
            if(await _lineUpRepository.EntityExists(bookingToUpdate) == false)
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
            return Ok();
        }
    }
}