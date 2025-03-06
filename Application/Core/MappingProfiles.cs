using Application.DTOs;
using AutoMapper;
using Domain;
using Domain.Enums;
using Domain.Models;

namespace Application.Core;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        // Ingredient Mapping
        
        // Ingredient => IngredientDto
        CreateMap<Ingredient, IngredientDto>()
            .ForMember(dest => dest.Category, opt =>
                opt.MapFrom(src => src.Category.ToString()))
            .ForMember(dest => dest.MeasuredIn, opt =>
                opt.MapFrom(src => src.MeasuredIn.ToString()))
            .ForMember(dest => dest.WeightUnit, opt =>
                opt.MapFrom(src => src.WeightUnit.ToString()))
            .ForMember(dest => dest.VolumeUnit, opt =>
                opt.MapFrom(src => src.VolumeUnit.ToString()))
            .ForMember(dest => dest.PricePerMeasurement, opt =>
                opt.MapFrom(src => src.PricePerPackage / src.Quantity));
        
        // IngredientDto => Ingredient
        CreateMap<IngredientDto, Ingredient>()
            .ForMember(dest => dest.Category, opt =>
                opt.MapFrom(src => Enum.Parse<Categories.IngredientCategory>(src.Category)))            
            .ForMember(dest => dest.MeasuredIn, opt =>
                opt.MapFrom(src => Enum.Parse<MeasurementUnits.MeasuredIn>(src.MeasuredIn)))            
            .ForMember(dest => dest.WeightUnit, opt =>
                opt.MapFrom(src => Enum.Parse<MeasurementUnits.WeightUnit>(src.WeightUnit)))            
            .ForMember(dest => dest.VolumeUnit, opt =>
                opt.MapFrom(src => Enum.Parse<MeasurementUnits.VolumeUnit>(src.VolumeUnit)))
            .ForMember(dest => dest.AppUserId, opt =>
                opt.MapFrom(src => src.AppUserId));
 


    }
    
    private static List<string> ConvertInstructionsJsonToList(string instructionsJson)
    {
        return string.IsNullOrEmpty(instructionsJson) ? new List<string>() : instructionsJson.Split(';').ToList();
    }
}
