using System.Collections.Generic;
using System.Linq;
using ltracker.Data.Repositories;
using VideoOnDemand.Data;
using VideoOnDemand.Entities;

namespace VideoOnDemand.Repositories
{
    public class SerieRepository : BaseRepository<Serie>
    {
        public SerieRepository(VideoOnDemandContext context) : base(context)
        {

        }

        public void InsertComplete(Serie serie, int[] actorId, int[] generoId)
        {
            if (actorId != null)
            {
                var actores = from a in _context.Actores
                              where actorId.Contains(a.Id.Value)
                              select a;

                serie.Actores = new List<Persona>();

                foreach (var a in actores)
                    serie.Actores.Add(a);
            }

            if (generoId != null)
            {
                var generos = from g in _context.Generos
                              where generoId.Contains(g.Id.Value)
                              select g;

                serie.Generos = new List<Genero>();
                foreach (var g in generos)
                    serie.Generos.Add(g);
            }

            if (generoId == null && actorId == null)
            {
                base.Insert(serie);
            }
            _context.Series.Add(serie);
        }

        public void UpdateComplete(Serie serie, int[] actorId, int[] generoId)
        {
            _context.Series.Attach(serie);
            _context.Entry(serie).State = System.Data.Entity.EntityState.Modified;

            //Instruccion para recargar una coleccion de mi entidad
            _context.Entry(serie).Collection(x => x.Generos).Load();
            _context.Entry(serie).Collection(x => x.Actores).Load();

            //Borra lo anterior y agrega lo nuevo, por si el usuario haia selccionado 3  ahora solo queire sleccionar 2
            serie.Generos.Clear();
            serie.Actores.Clear();

            if (actorId != null)
            {
                var persona = from a in _context.Actores
                              where actorId.Contains((int)a.Id)
                              select a;
                serie.Actores = new List<Persona>();
                foreach (var p in persona)
                    serie.Actores.Add(p);
            }

            if (generoId != null)
            {
                var generos = from g in _context.Generos
                              where generoId.Contains((int)g.Id)
                              select g;
                serie.Generos = new List<Genero>();
                foreach (var g in generos)
                    serie.Generos.Add(g);
            }



        }
    }
}
