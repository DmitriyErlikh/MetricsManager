using AutoMapper;
using MetricsAgent.DAL;
using MetricsAgent.Models;
using MetricsAgent.Responses;

namespace MetricsAgent.DAL
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CpuMetric, CpuMetricDto>().ForSourceMember(
                x => x.Id, opt => opt.DoNotValidate());

            CreateMap<DotnetMetricDto, DotnetMetricDto>().ForSourceMember(
                x => x.Id, opt => opt.DoNotValidate());

            CreateMap<HDDMetricDto, HDDMetricDto>().ForSourceMember(
                x => x.Id, opt => opt.DoNotValidate());

            CreateMap<NetworkMetric, NetworkMetricDto>().ForSourceMember(
                x => x.Id, opt => opt.DoNotValidate());

            CreateMap<RAMMetricDto, RAMMetricDto>().ForSourceMember(
                x => x.Id, opt => opt.DoNotValidate());
        }
    }
}