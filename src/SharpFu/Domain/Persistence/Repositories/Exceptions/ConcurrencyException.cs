#region

using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

#endregion

namespace SharpFu.Domain.Persistence.Repositories.Exceptions
{

	
	[Serializable]
	public class ConcurrencyException : RepositoryException
	{
		public ConcurrencyException(Type entityType, object keyValue)
			: this(entityType, null, keyValue)
		{
		}

		public ConcurrencyException(Type entityType, Type keyType, object keyValue)
			: this(String.Format("Entity with id {0} already available", keyValue), entityType, keyType, keyValue)
		{
		}

		public ConcurrencyException(string message, Type entityType, object keyValue)
			: this(message, entityType, null, keyValue)
		{
		}

		public ConcurrencyException(string message, Type entityType, Type keyType, object keyValue)
			: this(message, entityType, keyType, keyValue, null)
		{
		}

		public ConcurrencyException(string message, Type entityType, Type keyType, object keyValue, Exception inner)
			: base(message, entityType, keyType, inner)
		{
			KeyValue = keyValue;
		}

		protected ConcurrencyException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			KeyValue = info.GetValue("KeyValue", typeof (object));
		}

		public object KeyValue { get; set; }

		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}

			info.AddValue("KeyValue", KeyValue);

			base.GetObjectData(info, context);
		}
	}
}