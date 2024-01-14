using System.Runtime.Serialization;

namespace Autopark.gRPC.Responses;

[DataContract]
public class VehicleResponse
{
	[DataMember(Order = 1)]
	public int Id { get; set; }
	
	[DataMember(Order = 2)]
	public int AutoparkId { get; set; }
}