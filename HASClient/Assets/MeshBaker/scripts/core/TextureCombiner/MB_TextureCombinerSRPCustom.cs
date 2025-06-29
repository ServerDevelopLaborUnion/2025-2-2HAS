using UnityEngine;
using UnityEngine.Rendering;
using System.Collections.Generic;

namespace DigitalOpus.MB.Core
{
    /// <summary>
    /// The scriptable rendering pipelines need special attention when assigning texture properties.
    /// For most maps there are keywords that need to be set in the material to tell the shader to use the property.
    /// </summary>
    public static class MB_TextureCombinerSRPCustom
    {
        static bool IsURPMaterial(Material m)
        {
            if (m.HasProperty("_BaseMap"))
            {
                return true;
            }

            return false;
        }

        static internal void ConfigureMaterialKeywordsIfNecessary(MB3_TextureCombinerPipeline.TexturePipelineData data)
        {
            if (MBVersion.DetectPipeline() == MBVersion.PipelineType.URP)
            {
                if (IsURPMaterial(data.resultMaterial))
                {
                    MB_TextureCombinerSRPCustom_URP.ConfigureMaterialKeywords(data, data.resultMaterial);
                }
            }

            if (MBVersion.DetectPipeline() == MBVersion.PipelineType.Default)
            {
                if (data.resultMaterial != null && data.resultMaterial.name.Contains("Standard"))
                {
                    MB_TextureCombinerSRPCustom_Standard.ConfigureMaterialKeywords(data, data.resultMaterial);
                }
            }
        }
    }

    public static class MB_TextureCombinerSRPCustom_URP
    {
        static bool _IsCreatingAtlasForProperty(MB3_TextureCombinerPipeline.TexturePipelineData data, string property)
        {
            for (int propIdx = 0; propIdx < data.texPropertyNames.Count; propIdx++)
            {
                if (property.Equals(data.texPropertyNames[propIdx].name))
                {
                    if (MB3_TextureCombinerPipeline._ShouldWeCreateAtlasForThisProperty(propIdx, data._considerNonTextureProperties, data.allTexturesAreNullAndSameColor))
                    {
                        return true;
                    } else
                    {
                        return false;
                    }
                }
            }

            return false;
        }

        static internal void ConfigureMaterialKeywords(MB3_TextureCombinerPipeline.TexturePipelineData data, Material resultMat)
        {
            if (MBVersion.IsMaterialKeywordValid(resultMat, "_NORMALMAP"))
            {
                if (_IsCreatingAtlasForProperty(data, "_BumpMap"))
                {
                    resultMat.EnableKeyword("_NORMALMAP");
                } else
                {
                    resultMat.DisableKeyword("_NORMALMAP");
                }
            }

            if (MBVersion.IsMaterialKeywordValid(resultMat, "_SPECGLOSSMAP"))
            {
                bool creatingSpecGloss = _IsCreatingAtlasForProperty(data, "_SpecGlossMap");
#if DEBUG || UNITY_EDITOR
                if (creatingSpecGloss && MBVersion.IsMaterialKeywordValid(resultMat, "_SPECULAR_SETUP"))
                {
                    Debug.Assert(resultMat.IsKeywordEnabled("_SPECULAR_SETUP"), "Generating SpecGlossMap for URP material but 'Specular Setup' has not been set.");
                }

                if (creatingSpecGloss)
                {
                    Debug.Assert(!_IsCreatingAtlasForProperty(data, "_MetallicGlossMap"), "Should not be generating atlases for both _SpecGlossMap and _MetallicGlossMap for a URP result material");
                }
#endif

                if (_IsCreatingAtlasForProperty(data, "_SpecGlossMap"))
                {
                    resultMat.EnableKeyword("_SPECGLOSSMAP");
                }
                else
                {
                    resultMat.DisableKeyword("_SPECGLOSSMAP");
                }
            }

            if (MBVersion.IsMaterialKeywordValid(resultMat, "_METALLICSPECGLOSSMAP"))
            {
                bool creatingMetallicGloss = _IsCreatingAtlasForProperty(data, "_MetallicGlossMap");
#if DEBUG || UNITY_EDITOR
                if (creatingMetallicGloss && MBVersion.IsMaterialKeywordValid(resultMat, "_SPECULAR_SETUP"))
                {
                    Debug.Assert(!resultMat.IsKeywordEnabled("_SPECULAR_SETUP"), "Generating Matallic map for URP material but 'Specular Setup' has been set.");
                }

                if (creatingMetallicGloss)
                {
                    Debug.Assert(!_IsCreatingAtlasForProperty(data, "_SpecGlossMap"), "Should not be generating atlases for both _SpecGlossMap and _MetallicGlossMap for a URP result material");
                }
#endif

                if (creatingMetallicGloss)
                {
                    resultMat.EnableKeyword("_METALLICSPECGLOSSMAP");
                }
                else
                {
                    resultMat.DisableKeyword("_METALLICSPECGLOSSMAP");
                }
            }

            if (MBVersion.IsMaterialKeywordValid(resultMat, "_PARALLAXMAP"))
            {
                if (_IsCreatingAtlasForProperty(data, "_ParallaxMap"))
                {
                    resultMat.EnableKeyword("_PARALLAXMAP");
                }
                else
                {
                    resultMat.DisableKeyword("_PARALLAXMAP");
                }
            }

            if (MBVersion.IsMaterialKeywordValid(resultMat, "_OCCLUSIONMAP"))
            {
                if (_IsCreatingAtlasForProperty(data, "_OcclusionMap"))
                {
                    resultMat.EnableKeyword("_OCCLUSIONMAP");
                }
                else
                {
                    resultMat.DisableKeyword("_OCCLUSIONMAP");
                }
            }
        }
    }


    public static class MB_TextureCombinerSRPCustom_Standard
    {
        static bool _IsCreatingAtlasForProperty(MB3_TextureCombinerPipeline.TexturePipelineData data, string property)
        {
            for (int propIdx = 0; propIdx < data.texPropertyNames.Count; propIdx++)
            {
                if (property.Equals(data.texPropertyNames[propIdx].name))
                {
                    if (MB3_TextureCombinerPipeline._ShouldWeCreateAtlasForThisProperty(propIdx, data._considerNonTextureProperties, data.allTexturesAreNullAndSameColor))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            return false;
        }

        static internal void ConfigureMaterialKeywords(MB3_TextureCombinerPipeline.TexturePipelineData data, Material resultMat)
        {
            // Saddly this doesn't work at runtime. For some unknown reason
            // The Standard shader is black unless I assign a texture to MainTex using the inspector.


            /*
            if (MBVersion.IsMaterialKeywordValid(resultMat, "_MainTex"))
            {
                if (_IsCreatingAtlasForProperty(data, "_MainTex"))
                {
                    resultMat.EnableKeyword("");
                }
                else
                {
                    resultMat.DisableKeyword("");
                }
            }
            */

            if (MBVersion.IsMaterialKeywordValid(resultMat, "_NORMALMAP"))
            {
                if (_IsCreatingAtlasForProperty(data, "_BumpMap"))
                {
                    resultMat.EnableKeyword("_NORMALMAP");
                }
                else
                {
                    resultMat.DisableKeyword("_NORMALMAP");
                }
            }

            if (MBVersion.IsMaterialKeywordValid(resultMat, "_METALLICGLOSSMAP"))
            {
                if (_IsCreatingAtlasForProperty(data, "_MetallicGlossMap"))
                {
                    resultMat.EnableKeyword("_METALLICGLOSSMAP");
                }
                else
                {
                    resultMat.DisableKeyword("_METALLICGLOSSMAP");
                }
            }

            if (MBVersion.IsMaterialKeywordValid(resultMat, "_METALLICSPECGLOSSMAP"))
            {
                bool creatingMetallicGloss = _IsCreatingAtlasForProperty(data, "_MetallicGlossMap");
#if DEBUG || UNITY_EDITOR
                if (creatingMetallicGloss && MBVersion.IsMaterialKeywordValid(resultMat, "_SPECULAR_SETUP"))
                {
                    Debug.Assert(!resultMat.IsKeywordEnabled("_SPECULAR_SETUP"), "Generating Matallic map for URP material but 'Specular Setup' has been set.");
                }

                if (creatingMetallicGloss)
                {
                    Debug.Assert(!_IsCreatingAtlasForProperty(data, "_SpecGlossMap"), "Should not be generating atlases for both _SpecGlossMap and _MetallicGlossMap for a URP result material");
                }
#endif

                if (creatingMetallicGloss)
                {
                    resultMat.EnableKeyword("_METALLICSPECGLOSSMAP");
                }
                else
                {
                    resultMat.DisableKeyword("_METALLICSPECGLOSSMAP");
                }
            }

            if (MBVersion.IsMaterialKeywordValid(resultMat, "_PARALLAXMAP"))
            {
                if (_IsCreatingAtlasForProperty(data, "_ParallaxMap"))
                {
                    resultMat.EnableKeyword("_PARALLAXMAP");
                }
                else
                {
                    resultMat.DisableKeyword("_PARALLAXMAP");
                }
            }

            if (MBVersion.IsMaterialKeywordValid(resultMat, "_OCCLUSIONMAP"))
            {
                if (_IsCreatingAtlasForProperty(data, "_OcclusionMap"))
                {
                    resultMat.EnableKeyword("_OCCLUSIONMAP");
                }
                else
                {
                    resultMat.DisableKeyword("_OCCLUSIONMAP");
                }
            }

            if (MBVersion.IsMaterialKeywordValid(resultMat, "_EMISSIONMAP"))
            {
                if (_IsCreatingAtlasForProperty(data, "_EmissionMap"))
                {
                    resultMat.EnableKeyword("_EMISSIONMAP");
                }
                else
                {
                    resultMat.DisableKeyword("_EMISSIONMAP");
                }
            }
        }
    }
}
