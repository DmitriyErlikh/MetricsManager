using AutoMapper;
using MetricsAgent.Responses;
using MetricsManager.Models;


namespace MetricsManager.DAL
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CpuMetric, Responses.AllCpuMetricResponses.CpuMetricDto>().ForSourceMember(
                x => x.Id, opt => opt.DoNotValidate());

            CreateMap<DotnetMetric, Responses.AllDotnetMetricResponses.DotnetMetricDto>().ForSourceMember(
                x => x.Id, opt => opt.DoNotValidate());

            CreateMap<HDDMetric, Responses.AllHDDMetricResponses.HDDMetricDto>().ForSourceMember(
                x => x.Id, opt => opt.DoNotValidate());

            CreateMap<NetworkMetric, Responses.AllNetworkMetricResponses.NetworkMetricDto>().ForSourceMember(
                x => x.Id, opt => opt.DoNotValidate());

            CreateMap<RAMMetric, Responses.AllRAMMetricResponses.RAMMetricDto>().ForSourceMember(
                x => x.Id, opt => opt.DoNotValidate());
        }
    }
}