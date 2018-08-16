using AppFramework.Expressions;
using ltracker.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using VideoOnDemand.Data;
using VideoOnDemand.Entities;

namespace VideoOnDemand.Repositories
{
    public class MovieRepository : BaseRepository<Movie>
    {
        public MovieRepository(VideoOnDemandContext context) : base(context)
        {
        }

        public void InsertComplete(Movie movie, int[] actorId, int[] generoId)
        {
            if (actorId != null)
            {
                var actores = from a in _context.Actores
                              where actorId.Contains(a.Id.Value)
                              select a;
                
                movie.Actores = new List<Persona>();

                foreach (var a in actores)
                    movie.Actores.Add(a);
            }

            if (generoId != null)
            {
                var generos = from g in _context.Generos
                              where generoId.Contains(g.Id.Value)
                              select g;
                
                movie.Generos = new List<Genero>();
                foreach (var g in generos)
                    movie.Generos.Add(g);
            }

            if (generoId == null && actorId == null)
            {
                base.Insert(movie);
            }
            _context.Peliculas.Add(movie);
        }

        public void Insert(Opinion opinion)
        {
            throw new NotImplementedException();
        }

        public void UpdateComplete(Movie movie, int[] actorId, int[] generoId)
        {
            _context.Peliculas.Attach(movie);
            _context.Entry(movie).State = System.Data.Entity.EntityState.Modified;

            //Instruccion para recargar una coleccion de mi entidad
            _context.Entry(movie).Collection(x => x.Generos).Load();
            _context.Entry(movie).Collection(x => x.Actores).Load();

            //Borra lo anterior y agrega lo nuevo, por si el usuario haia selccionado 3  ahora solo queire sleccionar 2
            movie.Generos.Clear();
            movie.Actores.Clear();

            if (actorId != null)
            {
                var persona = from a in _context.Actores
                             where actorId.Contains((int)a.Id)
                             select a;
                movie.Actores = new List<Persona>();
                foreach (var p in persona)
                    movie.Actores.Add(p);
            }

            if (generoId != null)
            {
                var generos = from g in _context.Generos
                             where generoId.Contains((int)g.Id)
                             select g;
                movie.Generos = new List<Genero>();
                foreach (var g in generos)
                    movie.Generos.Add(g);
            }



        }

        public ICollection<Movie> QueryPageByNombreAndGeneroIncluding(string nombre, string genero, Expression<Func<Movie, object>>[] includes, out int totalPages, out int totalRows, string order, int page = 0, int pageSize = 10)
        {

            Expression<Func<Movie, bool>> where = s => true;
            where = where.And(s => s.Estatus == EEstatusMedia.VISIBLE);

            if (!String.IsNullOrEmpty(genero))
                where = where.And(s => s.Generos.Select(g => g.Nombre).Contains(genero));
            if (!String.IsNullOrEmpty(nombre))
                where = where.And(s => s.Nombre.Contains(nombre));

            int paginas;
            int filas;

            ICollection<Movie> movies = QueryPageIncluding(where, includes, out paginas, out filas, order, page, pageSize);

            totalPages = paginas;
            totalRows = filas;

            return movies;
        }

        public ICollection<Movie> QueryNombreAndGenero(string nombre, string genero, Expression<Func<Movie, object>>[] includes)
        {

            Expression<Func<Movie, bool>> where = s => true;
            where = where.And(s => s.Estatus != EEstatusMedia.ELIMINADO);

            if (!String.IsNullOrEmpty(genero))
                where = where.And(s => s.Generos.Select(g => g.Nombre).Contains(genero));
            if (!String.IsNullOrEmpty(nombre))
                where = where.And(s => s.Nombre.Contains(nombre));
            

            ICollection<Movie> movies = QueryIncluding(where, includes , "Nombre" );

            return movies;
        }


    }
}
