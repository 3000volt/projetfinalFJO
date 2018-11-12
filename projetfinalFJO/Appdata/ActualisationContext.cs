using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace projetfinalFJO.Appdata
{
    public partial class ActualisationContext : DbContext
    {
        private readonly string ConnectionString;

        public ActualisationContext()
        {
        }

        public ActualisationContext(DbContextOptions<ActualisationContext> options)
            : base(options)
        {
        }

        public ActualisationContext(string connexion)
        {
            this.ConnectionString = connexion;
        }

        public void InsererActualisation(ActualisationInformation actu)
        {
            //utiliser le connectionString pour pouvoir affecter la BD
            using (SqlConnection con = new SqlConnection(this.ConnectionString))
            {
                //Requete pour ajouter un livre
                string sqlStr = "insert into ActualisationInformation(NumActualisation, NomActualisation, NoProgramme, Approuve) values(@NumActualisation, @NomActualisation, @NoProgramme, @Approuve)";
                //Code pour affecter la BD
                SqlCommand cmd = new SqlCommand(sqlStr, con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                cmd.Parameters.AddWithValue("NumActualisation", actu.NumActualisation);
                cmd.Parameters.AddWithValue("NomActualisation", actu.NomActualisation);
                cmd.Parameters.AddWithValue("NoProgramme", actu.NoProgramme);
                //Par defaut, le programme ne sera pas approuvé
                cmd.Parameters.AddWithValue("Approuve", false);
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void InsererMembresdesactualisations(Membresdesactualisations me)
        {
            //utiliser le connectionString pour pouvoir affecter la BD
            using (SqlConnection con = new SqlConnection(this.ConnectionString))
            {
                //Requete pour ajouter un livre
                string sqlStr = "insert into Membresdesactualisations(NumActualisation, AdresseCourriel) values(@NumActualisation, @AdresseCourriel)";
                //Code pour affecter la BD
                SqlCommand cmd = new SqlCommand(sqlStr, con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                cmd.Parameters.AddWithValue("NumActualisation", me.NumActualisation);
                cmd.Parameters.AddWithValue("AdresseCourriel", me.AdresseCourriel);
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void SupprimerActualisation(int numAct)
        {
            //utiliser le connectionString pour pouvoir affecter la BD
            using (SqlConnection con = new SqlConnection(this.ConnectionString))
            {
                //requete pour supprimer un livre
                string sqlStr = "delete from ActualisationInformation where NumActualisation = @numActualisation";
                //Code pour Affecter la BD
                SqlCommand cmd = new SqlCommand(sqlStr, con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                //Associer la valeur de l isbn en paramettre
                cmd.Parameters.AddWithValue("numActualisation", numAct);
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void RetirerMembreActu(int numAct, string email)
        {
            //utiliser le connectionString pour pouvoir affecter la BD
            using (SqlConnection con = new SqlConnection(this.ConnectionString))
            {
                //requete pour supprimer un livre
                string sqlStr = "delete from Membresdesactualisations where NumActualisation = @numActualisation and AdresseCourriel = @adresseCourriel";
                //Code pour Affecter la BD
                SqlCommand cmd = new SqlCommand(sqlStr, con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                //Associer la valeur de l isbn en paramettre
                cmd.Parameters.AddWithValue("numActualisation", numAct);
                cmd.Parameters.AddWithValue("adresseCourriel", email);
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void SupprimerUtilisateur(string email)
        {
            //utiliser le connectionString pour pouvoir affecter la BD
            using (SqlConnection con = new SqlConnection(this.ConnectionString))
            {
                //requete pour supprimer un livre
                string sqlStr = "delete from Utilisateur where AdresseCourriel = @adresseCourriel";
                //Code pour Affecter la BD
                SqlCommand cmd = new SqlCommand(sqlStr, con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                //Associer la valeur de l isbn en paramettre
                cmd.Parameters.AddWithValue("adresseCourriel", email);
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }


        public virtual DbSet<ActualisationInformation> ActualisationInformation { get; set; }
        public virtual DbSet<AnalyseCompétence> AnalyseCompétence { get; set; }
        public virtual DbSet<AnalyseElementsCompetence> AnalyseElementsCompetence { get; set; }
        public virtual DbSet<Commentaires> Commentaires { get; set; }
        public virtual DbSet<Competences> Competences { get; set; }
        public virtual DbSet<CompetencesElementCompetence> CompetencesElementCompetence { get; set; }
        public virtual DbSet<Cours> Cours { get; set; }
        public virtual DbSet<CoursCompetences> CoursCompetences { get; set; }
        public virtual DbSet<Elementcompetence> Elementcompetence { get; set; }
        public virtual DbSet<Famillecompetence> Famillecompetence { get; set; }
        public virtual DbSet<Membresdesactualisations> Membresdesactualisations { get; set; }
        public virtual DbSet<Prealables> Prealables { get; set; }
        public virtual DbSet<Programmes> Programmes { get; set; }
        public virtual DbSet<RepartirHeureCompetence> RepartirHeureCompetence { get; set; }
        public virtual DbSet<RepartitionHeureCours> RepartitionHeureCours { get; set; }
        public virtual DbSet<RepartitionHeuresession> RepartitionHeuresession { get; set; }
        public virtual DbSet<Session> Session { get; set; }
        public virtual DbSet<Utilisateur> Utilisateur { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(" Server=localhost;Database=Actualisation ;User Id=sa;Password=sql");
                //optionsBuilder.UseSqlServer(" Server=localhost; Database=Actualisation; Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ActualisationInformation>(entity =>
            {
                entity.HasKey(e => e.NumActualisation);

                entity.Property(e => e.NumActualisation).ValueGeneratedNever();

                entity.Property(e => e.NoProgramme)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.NomActualisation).HasMaxLength(50);

                entity.HasOne(d => d.NoProgrammeNavigation)
                    .WithMany(p => p.ActualisationInformation)
                    .HasForeignKey(d => d.NoProgramme)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Actualisa__NoPro__6C190EBB");
            });

            modelBuilder.Entity<AnalyseCompétence>(entity =>
            {
                entity.HasKey(e => e.IdAnalyseAc);

                entity.Property(e => e.IdAnalyseAc).HasColumnName("Id_Analyse_AC");

                entity.Property(e => e.AdresseCourriel)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.CodeCompetence)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.Context)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.NiveauTaxonomique)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Reformulation)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.SavoirEtreProgramme).HasColumnType("text");

                entity.Property(e => e.SavoirFaireProgramme).HasColumnType("text");

                entity.HasOne(d => d.AdresseCourrielNavigation)
                    .WithMany(p => p.AnalyseCompétence)
                    .HasForeignKey(d => d.AdresseCourriel)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__AnalyseCo__Adres__5BE2A6F2");

                entity.HasOne(d => d.CodeCompetenceNavigation)
                    .WithMany(p => p.AnalyseCompétence)
                    .HasForeignKey(d => d.CodeCompetence)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__AnalyseCo__CodeC__5CD6CB2B");
            });

            modelBuilder.Entity<AnalyseElementsCompetence>(entity =>
            {
                entity.HasKey(e => new { e.IdAnalyseAc, e.AdresseCourriel, e.Idelementcomp });

                entity.Property(e => e.IdAnalyseAc)
                    .HasColumnName("Id_Analyse_AC")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.AdresseCourriel).HasMaxLength(100);

                entity.Property(e => e.Context)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.NiveauTaxonomique)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Reformulation)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.SavoirEtreProgramme).HasColumnType("text");

                entity.Property(e => e.SavoirFaireProgramme).HasColumnType("text");

                entity.HasOne(d => d.IdelementcompNavigation)
                    .WithMany(p => p.AnalyseElementsCompetence)
                    .HasForeignKey(d => d.Idelementcomp)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__AnalyseEl__Idele__71D1E811");
            });

            modelBuilder.Entity<Commentaires>(entity =>
            {
                entity.HasKey(e => e.NumCom);

                entity.Property(e => e.NumCom).ValueGeneratedNever();

                entity.Property(e => e.AdresseCourriel)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Categorie)
                    .IsRequired()
                    .HasColumnName("categorie")
                    .HasMaxLength(50);

                entity.Property(e => e.TexteCom)
                    .IsRequired()
                    .HasColumnType("text");

                entity.HasOne(d => d.AdresseCourrielNavigation)
                    .WithMany(p => p.Commentaires)
                    .HasForeignKey(d => d.AdresseCourriel)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Commentai__Adres__571DF1D5");
            });

            modelBuilder.Entity<Competences>(entity =>
            {
                entity.HasKey(e => e.CodeCompetence);

                entity.Property(e => e.CodeCompetence)
                    .HasMaxLength(15)
                    .ValueGeneratedNever();

                entity.Property(e => e.ContextRealisation).HasColumnType("text");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.NoProgramme).HasMaxLength(15);

                entity.HasOne(d => d.IdfamilleNavigation)
                    .WithMany(p => p.Competences)
                    .HasForeignKey(d => d.Idfamille)
                    .HasConstraintName("FK__Competenc__Idfam__5629CD9C");

                entity.HasOne(d => d.NoProgrammeNavigation)
                    .WithMany(p => p.Competences)
                    .HasForeignKey(d => d.NoProgramme)
                    .HasConstraintName("FK__Competenc__NoPro__6EF57B66");
            });

            modelBuilder.Entity<CompetencesElementCompetence>(entity =>
            {
                entity.HasKey(e => new { e.CodeCompetence, e.Idelementcomp });

                entity.Property(e => e.CodeCompetence).HasMaxLength(15);

                entity.HasOne(d => d.CodeCompetenceNavigation)
                    .WithMany(p => p.CompetencesElementCompetence)
                    .HasForeignKey(d => d.CodeCompetence)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Competenc__CodeC__5FB337D6");

                entity.HasOne(d => d.IdelementcompNavigation)
                    .WithMany(p => p.CompetencesElementCompetence)
                    .HasForeignKey(d => d.Idelementcomp)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Competenc__Idele__60A75C0F");
            });

            modelBuilder.Entity<Cours>(entity =>
            {
                entity.HasKey(e => e.NoCours);

                entity.Property(e => e.NoCours)
                    .HasMaxLength(15)
                    .ValueGeneratedNever();

                entity.Property(e => e.DepartementCours)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.NoProgramme)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.NomCours)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PonderationCours)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.TypedeCours)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.IdsessionNavigation)
                    .WithMany(p => p.Cours)
                    .HasForeignKey(d => d.Idsession)
                    .HasConstraintName("FK__Cours__Idsession__59063A47");

                entity.HasOne(d => d.NoProgrammeNavigation)
                    .WithMany(p => p.Cours)
                    .HasForeignKey(d => d.NoProgramme)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Cours__NoProgram__5812160E");
            });

            modelBuilder.Entity<CoursCompetences>(entity =>
            {
                entity.HasKey(e => new { e.NoCours, e.CodeCompetence });

                entity.Property(e => e.NoCours).HasMaxLength(15);

                entity.Property(e => e.CodeCompetence).HasMaxLength(15);

                entity.Property(e => e.NbHcoursCompetence).HasColumnName("NbHCoursCompetence");

                entity.HasOne(d => d.CodeCompetenceNavigation)
                    .WithMany(p => p.CoursCompetences)
                    .HasForeignKey(d => d.CodeCompetence)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CoursComp__CodeC__5AEE82B9");

                entity.HasOne(d => d.NoCoursNavigation)
                    .WithMany(p => p.CoursCompetences)
                    .HasForeignKey(d => d.NoCours)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CoursComp__NoCou__59FA5E80");
            });

            modelBuilder.Entity<Elementcompetence>(entity =>
            {
                entity.HasKey(e => e.Idelementcomp);

                entity.Property(e => e.Idelementcomp).ValueGeneratedNever();

                entity.Property(e => e.CriterePerformance)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.ElementCompétence)
                    .IsRequired()
                    .HasColumnType("text");
            });

            modelBuilder.Entity<Famillecompetence>(entity =>
            {
                entity.HasKey(e => e.Idfamille);

                entity.Property(e => e.Idfamille).ValueGeneratedNever();

                entity.Property(e => e.NomFamille)
                    .IsRequired()
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<Membresdesactualisations>(entity =>
            {
                entity.HasKey(e => new { e.NumActualisation, e.AdresseCourriel });

                entity.Property(e => e.AdresseCourriel).HasMaxLength(100);

                entity.HasOne(d => d.AdresseCourrielNavigation)
                    .WithMany(p => p.Membresdesactualisations)
                    .HasForeignKey(d => d.AdresseCourriel)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Membresde__Adres__6E01572D");

                entity.HasOne(d => d.NumActualisationNavigation)
                    .WithMany(p => p.Membresdesactualisations)
                    .HasForeignKey(d => d.NumActualisation)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Membresde__NumAc__6D0D32F4");
            });

            modelBuilder.Entity<Prealables>(entity =>
            {
                entity.HasKey(e => new { e.NoCoursPrealable, e.NoCours });

                entity.Property(e => e.NoCoursPrealable)
                    .HasColumnName("NoCours_prealable")
                    .HasMaxLength(15);

                entity.Property(e => e.NoCours).HasMaxLength(15);

                entity.HasOne(d => d.NoCoursNavigation)
                    .WithMany(p => p.Prealables)
                    .HasForeignKey(d => d.NoCours)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Prealable__NoCou__628FA481");
            });

            modelBuilder.Entity<Programmes>(entity =>
            {
                entity.HasKey(e => e.NoProgramme);

                entity.Property(e => e.NoProgramme)
                    .HasMaxLength(15)
                    .ValueGeneratedNever();

                entity.Property(e => e.CondtionsAdmission).HasColumnType("text");

                entity.Property(e => e.NomProgramme).HasMaxLength(100);
            });

            modelBuilder.Entity<RepartirHeureCompetence>(entity =>
            {
                entity.HasKey(e => new { e.CodeCompetence, e.Idsession });

                entity.Property(e => e.CodeCompetence).HasMaxLength(15);

                entity.Property(e => e.NbHsessionCompetence).HasColumnName("NbHSessionCompetence");

                entity.HasOne(d => d.CodeCompetenceNavigation)
                    .WithMany(p => p.RepartirHeureCompetence)
                    .HasForeignKey(d => d.CodeCompetence)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__RepartirH__CodeC__5DCAEF64");

                entity.HasOne(d => d.IdsessionNavigation)
                    .WithMany(p => p.RepartirHeureCompetence)
                    .HasForeignKey(d => d.Idsession)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__RepartirH__Idses__5EBF139D");
            });

            modelBuilder.Entity<RepartitionHeureCours>(entity =>
            {
                entity.HasKey(e => e.IdAnalyseRhc);

                entity.Property(e => e.IdAnalyseRhc)
                    .HasColumnName("Id_Analyse_RHC")
                    .ValueGeneratedNever();

                entity.Property(e => e.AdresseCourriel)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.CodeCompetence)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.NoCours)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.HasOne(d => d.AdresseCourrielNavigation)
                    .WithMany(p => p.RepartitionHeureCours)
                    .HasForeignKey(d => d.AdresseCourriel)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Repartiti__Adres__68487DD7");

                entity.HasOne(d => d.CodeCompetenceNavigation)
                    .WithMany(p => p.RepartitionHeureCours)
                    .HasForeignKey(d => d.CodeCompetence)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Repartiti__CodeC__6A30C649");

                entity.HasOne(d => d.IdsessionNavigation)
                    .WithMany(p => p.RepartitionHeureCours)
                    .HasForeignKey(d => d.Idsession)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Repartiti__Idses__6B24EA82");

                entity.HasOne(d => d.NoCoursNavigation)
                    .WithMany(p => p.RepartitionHeureCours)
                    .HasForeignKey(d => d.NoCours)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Repartiti__NoCou__693CA210");
            });

            modelBuilder.Entity<RepartitionHeuresession>(entity =>
            {
                entity.HasKey(e => e.IdAnalyseRhs);

                entity.Property(e => e.IdAnalyseRhs)
                    .HasColumnName("Id_Analyse_RHS")
                    .ValueGeneratedNever();

                entity.Property(e => e.AdresseCourriel)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.CodeCompetence)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.NoCours)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.HasOne(d => d.AdresseCourrielNavigation)
                    .WithMany(p => p.RepartitionHeuresession)
                    .HasForeignKey(d => d.AdresseCourriel)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Repartiti__Adres__6383C8BA");

                entity.HasOne(d => d.CodeCompetenceNavigation)
                    .WithMany(p => p.RepartitionHeuresession)
                    .HasForeignKey(d => d.CodeCompetence)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Repartiti__CodeC__6477ECF3");

                entity.HasOne(d => d.IdsessionNavigation)
                    .WithMany(p => p.RepartitionHeuresession)
                    .HasForeignKey(d => d.Idsession)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Repartiti__Idses__66603565");

                entity.HasOne(d => d.NoCoursNavigation)
                    .WithMany(p => p.RepartitionHeuresession)
                    .HasForeignKey(d => d.NoCours)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Repartiti__NoCou__6754599E");
            });

            modelBuilder.Entity<Session>(entity =>
            {
                entity.HasKey(e => e.Idsession);

                entity.Property(e => e.Idsession).ValueGeneratedNever();

                entity.Property(e => e.NomSession)
                    .IsRequired()
                    .HasMaxLength(15);
            });

            modelBuilder.Entity<Utilisateur>(entity =>
            {
                entity.HasKey(e => e.AdresseCourriel);

                entity.Property(e => e.AdresseCourriel)
                    .HasMaxLength(100)
                    .ValueGeneratedNever();

                entity.Property(e => e.Nom)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Prenom)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.RegisterDate).HasColumnType("date");
            });
        }
    }
}
