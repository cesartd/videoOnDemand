using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoOnDemand.Entities;

namespace VideoOnDemand.Data
{
    public class VideoOnDemandContext : DbContext
    {
        public VideoOnDemandContext() : base("name=VideoOnDemandContext")
        {
            this.Configuration.LazyLoadingEnabled = true;
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Media> Medias { get; set; }
        public DbSet<Genero> Generos { get; set; }
        public DbSet<Serie> Series { get; set; }
        public DbSet<Persona> Actores { get; set; }
        public DbSet<Opinion> Opiniones { get; set; }
        public DbSet<Favorito> Favoritos { get; set; }
        public DbSet<MediaOnPlay> MediasOnPlay { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var usuarioEntity = modelBuilder.Entity<Usuario>();
            var mediaEntity = modelBuilder.Entity<Media>();
            var generoEntity = modelBuilder.Entity<Genero>();
            var serieEntity = modelBuilder.Entity<Serie>();
            var personaEntity = modelBuilder.Entity<Persona>();
            var opinionEntity = modelBuilder.Entity<Opinion>();
            var favoritoEntity = modelBuilder.Entity<Favorito>();
            var mediaOnPlayEntity = modelBuilder.Entity<MediaOnPlay>();

            usuarioEntity.HasKey(x => x.Id);
            usuarioEntity.Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            usuarioEntity.Property(x => x.Nombre).HasMaxLength(200).IsRequired();
            usuarioEntity.Property(x => x.IdentityId).HasMaxLength(128).IsRequired();

            mediaEntity.HasKey(x => x.Id);
            mediaEntity.Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            mediaEntity.Property(x => x.Nombre).HasMaxLength(50).IsRequired();
            mediaEntity.Property(x => x.Descripcion).HasMaxLength(500).IsOptional();


            #region TPT movie, serie y episodio
            modelBuilder.Entity<Movie>().ToTable("Movie");
            modelBuilder.Entity<Episodio>().ToTable("Episodio");
            serieEntity.ToTable("Serie");
            #endregion

            generoEntity.HasKey(x => x.Id);
            generoEntity.Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            generoEntity.Property(x => x.Nombre).HasMaxLength(50).IsRequired();
            generoEntity.Property(x => x.Descripcion).HasMaxLength(100).IsOptional();

            personaEntity.HasKey(x => x.Id);
            personaEntity.Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            personaEntity.Property(x => x.Nombre).HasMaxLength(50).IsRequired();
            personaEntity.Property(x => x.Descripcion).HasMaxLength(500).IsOptional();

            opinionEntity.HasKey(x => x.Id);
            opinionEntity.Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            opinionEntity.Property(x => x.Puntuacion).IsRequired();
            opinionEntity.Property(x => x.Descripcion).HasMaxLength(500).IsOptional();

            favoritoEntity.HasKey(x => x.Id);
            favoritoEntity.Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            mediaOnPlayEntity.HasKey(x => x.Id);
            mediaOnPlayEntity.Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            #region one-to-many Serie-episodios
            serieEntity.HasMany(a => a.Episodios)
                .WithRequired(b => b.Serie)
                .HasForeignKey(b => b.SerieId);
            #endregion

            #region many-to-many Media-genero
            mediaEntity.HasMany(a => a.Generos)
                .WithMany()
                .Map(t =>
                {
                    t.MapLeftKey("MediaId");
                    t.MapRightKey("GeneroId");
                    t.ToTable("MediaGenero");
                });
            #endregion

            #region many-to-many Media-Persona(Actor)
            mediaEntity.HasMany(a => a.Actores)
                .WithMany()
                .Map(t =>
                {
                    t.MapLeftKey("MediaId");
                    t.MapRightKey("ActorId");
                    t.ToTable("MediaActor");
                });
            #endregion

            #region one-to-many opinion-media
            opinionEntity.HasRequired(a => a.Media)
                .WithMany()
                .HasForeignKey(a => a.MediaId);
            #endregion

            #region one-to-many opinion-usuario
            opinionEntity.HasRequired(a => a.Usuario)
                .WithMany()
                .HasForeignKey(a => a.UsuarioId);
            #endregion

            #region one-to-zero-or-one favorito-media
            favoritoEntity.HasRequired(a => a.Media)
                .WithMany().HasForeignKey( a => a.MediaId);
            #endregion

            #region one-to-many favorito-usuario
            favoritoEntity.HasRequired(a => a.Usuario)
                .WithMany()
                .HasForeignKey(a => a.UsuarioId);
            #endregion

            #region one-to-zero-or-one mediaOnPlay-media
            mediaOnPlayEntity.HasRequired(a => a.Media)
                .WithMany()
                .HasForeignKey(a => a.MediaId);
            #endregion

            #region one-to-many mediaOnPlay-usuario
            mediaOnPlayEntity.HasRequired(a => a.Usuario)
                .WithMany()
                .HasForeignKey(a => a.UsuarioId);
            #endregion
        }
    }
}
