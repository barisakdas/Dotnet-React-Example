namespace ApiLayer.Controllers
{
    public class HotelController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IService<HotelEntity> _service;

        public HotelController(IMapper mapper, IService<HotelEntity> service) => (_mapper, _service) = (mapper, service);


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var hotels = await _service.GetAll();
            var hotelDtos = _mapper.Map<List<HotelDto>>(hotels.ToList());

            return CreateActionResult(CustomResponseDto<List<HotelDto>>.Success(200, hotelDtos));
        }


        [ServiceFilter(typeof(NotFoundFilter<HotelEntity>))]    // Bu filterı normal bir attribute olarak kullanamazsınız. Constructor da parametre alan bir filter veya attribute kullanacağınız zaman ServiceFilter üzerinden kullanmalısınız.
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var hotel = await _service.GetByIdAsync(id);
            var hotelDto = _mapper.Map<HotelDto>(hotel);

            return CreateActionResult(CustomResponseDto<HotelDto>.Success(200, hotelDto));
        }

        [HttpPost]
        public async Task<IActionResult> Save(HotelCreateDto hotelDto)
        {
            var hotel = await _service.AddAsync(_mapper.Map<HotelEntity>(hotelDto));
            var response = _mapper.Map<HotelDto>(hotel);

            return CreateActionResult(CustomResponseDto<HotelDto>.Success(201, response));
        }

        [HttpPut]
        public async Task<IActionResult> Update(HotelUpdateDto hotelDto)
        {
            await _service.UpdateAsync(_mapper.Map<HotelEntity>(hotelDto));

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        [ServiceFilter(typeof(NotFoundFilter<HotelEntity>))]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var hotel = await _service.GetByIdAsync(id);
            await _service.DeleteAsync(hotel);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }
    }
}
