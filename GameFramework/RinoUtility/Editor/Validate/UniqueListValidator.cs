﻿#if ODIN_INSPECTOR
using GameFramework.RinoUtility.Attribute;
using GameFramework.RinoUtility.Editor.Validate;
using Sirenix.OdinInspector.Editor;
using Sirenix.OdinInspector.Editor.Validation;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[assembly : RegisterValidator(typeof(UniqueListValidator<>))]
namespace GameFramework.RinoUtility.Editor.Validate
{
	public class UniqueListValidator<T>: AttributeValidator<UniqueListAttribute, T>
	{
		public override bool CanValidateProperty(InspectorProperty property) => typeof(IList).IsAssignableFrom(property.ParentType);

		protected override void Validate(ValidationResult result)
		{
			if(ValueEntry.SmartValue == null)
			{
				return;
			}

			var list = (List<T>)(Property.Parent.ValueEntry.WeakSmartValue);

			if(list.Count(x => x.Equals(ValueEntry.SmartValue)) > 1)
			{
				result.ResultType = ValidationResultType.Error;
				result.Message = $@"{Attribute.ErrorMessage}";
			}
		}
	}
}
#endif