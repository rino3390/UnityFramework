using RinoGameFramework.Utility;
using Sirenix.OdinInspector;
using System;

namespace RinoGameFramework.Localize
{
	[Serializable]
	public class LanguageType
	{
		public string Id { get; private set; }

		[HideLabel]
		public string Language;

		public LanguageType()
		{
			Id = GUID.NewGuid();
		}

		public override bool Equals(object obj)
		{
			if(obj == null || GetType() != obj.GetType())
			{
				return false;
			}

			LanguageType other = (LanguageType)obj;
			return Language == other.Language;
		}

		public override int GetHashCode()
		{
			return Language.GetHashCode();
		}

		protected bool Equals(LanguageType other)
		{
			return Language == other.Language;
		}
	}
}