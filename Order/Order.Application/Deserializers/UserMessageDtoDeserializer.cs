using System.Text.Json;
using Confluent.Kafka;
using Order.Application.DTOs.UserDTOs;

namespace Order.Application.Deserializers;

public class UserMessageDtoDeserializer : IDeserializer<UserMessageDto>
{
	public UserMessageDto Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
	{
		return JsonSerializer.Deserialize<UserMessageDto>(data)!;
	}
}
