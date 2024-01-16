using System.Runtime.Serialization;

namespace Autopark.gRPC.Requests;

[DataContract]
public class TimeInterval
{
	[DataMember(Order = 1)]
	public DateTime Start { get; set; }
	
	[DataMember(Order = 2)]
	public DateTime Finish { get; set; }
}