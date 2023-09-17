using GameFramework.RinoUtility.Method;
using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace RinoLocalize.Common
{
	[Serializable]
	public class LanguageType
	{
		public string Id { get; private set; } = GUID.NewGuid();

		[HideLabel, Required("語言類型不能為空"),Delayed]
		public string Language = string.Empty;

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