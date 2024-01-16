using System.ServiceModel;
using Autopark.gRPC.Requests;
using Autopark.gRPC.Responses;

namespace Autopark.gRPC.Services;

[ServiceContract]
public interface IAutoparkService
{
	[OperationContract]
	Task<IEnumerable<VehicleResponse>> GetFreeCarsAsync(TimeInterval timeInterval);
	
	[OperationContract]
	Task<IEnumerable<VehicleResponse>> GetFreeTrailersAsync(TimeInterval timeInterval);
}
