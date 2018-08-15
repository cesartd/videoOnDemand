﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VideoOnDemand.Entities;
using VideoOnDemand.Web.Models;

namespace VideoOnDemand.Web.Helpers
{
    public class MapHelper
    {

        internal static IMapper mapper;

        static MapHelper()
        {
            var config = new MapperConfiguration(x => {
                x.CreateMap<Usuario, UsuarioViewModel>().ReverseMap();
                x.CreateMap<Genero, GeneroViewModel>().ReverseMap();
                x.CreateMap<Serie, SerieViewModel>().ReverseMap();
                x.CreateMap<Movie, MovieViewModel>().ReverseMap();
                x.CreateMap<Persona, PersonaViewModel>().ReverseMap();//Se crea el de persona
                x.CreateMap<Opinion, OpinionViewModel>().ReverseMap();
                x.CreateMap<Episodio, EpisodioViewModel>().ReverseMap();

               });
            mapper = config.CreateMapper();
        }

        public static T Map<T>(object source)
        {
            return mapper.Map<T>(source);
        }
    }
}