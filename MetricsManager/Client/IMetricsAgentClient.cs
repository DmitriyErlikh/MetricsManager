using MetricsManager.Responses;
using static MetricsManager.Requests.MetricsManager;

namespace MetricsManager.Client
{
    public interface IMetricsAgentClient
    {
        AllCpuMetricResponses GetAllCpuMetrics(CpuMetricsApiRequest request);

        AllDotnetMetricResponses GetAllDotnetMetrics(DotnetMetricsApiRequest request);

        AllHDDMetricResponses GetAllHDDMetrics(HDDMetricsApiRequest request);

        AllNetworkMetricResponses GetAllNetworkMetrics(NetworkMetricsApiRequest request);

        AllRAMMetricResponses GetAllRAMMetrics(RAMMetricsApiRequest request);
    }
}
