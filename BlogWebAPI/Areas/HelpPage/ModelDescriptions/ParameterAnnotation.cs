using System;

namespace BlogWebAPI.Areas.HelpPage.ModelDescriptions
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ParameterAnnotation'
    public class ParameterAnnotation
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ParameterAnnotation'
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ParameterAnnotation.AnnotationAttribute'
        public Attribute AnnotationAttribute { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ParameterAnnotation.AnnotationAttribute'

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ParameterAnnotation.Documentation'
        public string Documentation { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ParameterAnnotation.Documentation'
    }
}