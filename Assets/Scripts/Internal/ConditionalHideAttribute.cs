using System;
using UnityEngine;

namespace CrystalProject.Internal
{
    /// <summary>
    /// Hides/disables field in inspector.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class ConditionalHideAttribute : PropertyAttribute
    {
        public readonly string ConditionalSourceField;
        public readonly bool HideInInspector;

        /// <summary>
        /// Hides/disables field in inspector.
        /// </summary>
        /// <param name="conditionalSourceField">Name of field.</param>
        /// <param name="hideInInspector">True = hide; false = disable.</param>
        public ConditionalHideAttribute(string conditionalSourceField, bool hideInInspector = true)
        {
            ConditionalSourceField = conditionalSourceField;
            HideInInspector = hideInInspector;
        }
    }
}