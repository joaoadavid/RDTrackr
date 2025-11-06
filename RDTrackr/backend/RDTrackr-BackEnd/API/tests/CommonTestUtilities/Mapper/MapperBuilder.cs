using AutoMapper;
using CommonTestUtilities.IdEncryption;
using RDTrackR.Application.Services.AutoMapper;

namespace CommonTestUtilities.Mapper
{
    public static class MapperBuilder
    {
        public static IMapper Build()
        {
            var idEncripter = IdEncripterBuilder.Build();

            return new AutoMapper.MapperConfiguration(options =>
            {
                options.AddProfile(new AutoMapping(idEncripter));
            }).CreateMapper();
        }
    }
}
