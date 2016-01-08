namespace SqlGenerator.Entities
{
	namespace Enums
	{
		public enum CommandType
		{
			Insert,
			Update,
			Delete,
		}

		public enum SourceType
		{
			Excel,
			Text,
            Sql
		}

		public enum DelimiterType
		{
			Comma,
			Semicolon,
			Tab,
		}

	}
}