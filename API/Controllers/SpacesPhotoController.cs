using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Controllers.DTOs;
using API.Core;
using API.Core.Models;
using API.Extension;
using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace API.Controllers {
    [Route ("api/photos/{userId}")]
    [ApiController]
    public class SpacesPhotoController : ControllerBase {
        public SpacesPhotoController (PhotoSettings _photoSettings, IMapper _mapper, ILineUpRepository _repository) {
            this._photoSettings = _photoSettings;
            this._mapper = _mapper;
            this._repository = _repository;

        }
        private PhotoSettings _photoSettings { get; }
        private IMapper _mapper { get; }
        private ILineUpRepository _repository { get; }
        private IOptions<CloudinarySettings> _cloudinaryConfig { get; }
        private Cloudinary _cloudinary;
        public SpacesPhotoController (IMapper mapper, ILineUpRepository repository, IOptions<CloudinarySettings> cloudinaryConfig, IOptionsSnapshot<PhotoSettings> options) {
            _cloudinaryConfig = cloudinaryConfig;
            _repository = repository;
            _mapper = mapper;
            _photoSettings = options.Value;

            Account acc = new Account (
                _cloudinaryConfig.Value.CloudName,
                _cloudinaryConfig.Value.ApiKey,
                _cloudinaryConfig.Value.ApiSecret
            );

            _cloudinary = new Cloudinary (acc);
        }

        [HttpGet]
        public ActionResult<IEnumerable<string>> Get () {
            return new string[] { "value1", "value2" };
        }

        [HttpGet ("{id}")]
        public async Task<IActionResult> GetMainPhoto (int id) {
            var photo = await _repository.GetMainPhoto (id);
            var photoToReturn = _mapper.Map<PhotoToReturnDTO> (photo);
            return Ok (photoToReturn);
        }

        [HttpPost ("{id}")]
        public async Task<IActionResult> AddPhoto (int userId, int id, [FromForm] PhotoForCreationDTO photoForCreationDTO) {
            if (userId != int.Parse (User.FindFirst (ClaimTypes.NameIdentifier).Value)) {
                return Unauthorized ();
            }
            var space = await _repository.GetSpace (id);
            if (space == null)
                return BadRequest ("Cannot upload photo for unexisting entity");

            var file = photoForCreationDTO.File;
            if (file.Length > _photoSettings.MaxBytes) return BadRequest ("Maximum file size exceeded");
            if (!_photoSettings.isSupported (file.FileName)) return BadRequest ("Invalid file type.");
            var uploadResult = new ImageUploadResult ();

            if (file.Length > 0) {
                using (var stream = file.OpenReadStream ()) {
                    var uploadParams = new ImageUploadParams () {
                    File = new FileDescription (file.Name, stream),
                    // Transformation = new Transformation()
                    //     .Width(500).Height(500).Crop("fill").Gravity("face")
                    };

                    uploadResult = _cloudinary.Upload (uploadParams);
                }
            }
            photoForCreationDTO.FileName = uploadResult.Uri.ToString ();
            photoForCreationDTO.PublicId = uploadResult.PublicId;
            photoForCreationDTO.SpaceId = id;

            var photo = _mapper.Map<Photo> (photoForCreationDTO);

            space.Photos.Add (photo);
            // _repository.Add(photo);

            if (await _repository.SaveAllChanges ()) {
                var photoToReturn = _mapper.Map<PhotoToReturnDTO> (photo);
                return Ok (photoToReturn);
            }

            return BadRequest ("Could not add photo");
        }

        [HttpPost ("setMain")]
        public async Task<IActionResult> SetMain (int userId, SetMainPhotoId setMainPhoto) {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)) {
                return Unauthorized();
            }
            var photo = await _repository.GetPhoto (setMainPhoto.NewMainId);

            if (photo == null)
                return BadRequest ("Photo does not exist");
            if (photo.IsMain)
                return BadRequest ("Photo is already main photo");

            if (setMainPhoto.CurrentMainId != 0) {
                var currentMainPhoto = await _repository.GetMainPhoto (setMainPhoto.CurrentMainId);

                if (currentMainPhoto != null) {
                    currentMainPhoto.IsMain = false;
                }
            }

            photo.IsMain = true;

            if (await _repository.SaveAllChanges ())
                return Ok ();
            return BadRequest ("Could not set main photo");
        }

        [HttpDelete ("{id}")]
        public async Task<IActionResult> DeletePhoto (int userId, int id) {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)) {
                return Unauthorized();
            }
            var photoToDelete = await _repository.GetPhoto (id);

            if (photoToDelete.IsMain)
                return BadRequest ("You can't delete the main photo");

            if (photoToDelete.PublicId != null) {
                var deleteParams = new DeletionParams (photoToDelete.PublicId);

                var result = _cloudinary.Destroy (deleteParams);

                if (result.Result == "ok") {
                    _repository.Delete (photoToDelete);
                }
            }

            if (photoToDelete.PublicId == null) {
                _repository.Delete (photoToDelete);
            }

            if (await _repository.SaveAllChanges ())
                return Ok ();

            return BadRequest ("Failed to delete photo");
        }
    }
}