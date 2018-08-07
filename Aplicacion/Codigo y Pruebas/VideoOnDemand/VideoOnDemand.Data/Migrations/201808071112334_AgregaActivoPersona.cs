namespace VideoOnDemand.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgregaActivoPersona : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Personas", "Activo", c => c.Boolean());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Personas", "Activo", c => c.Int());
        }
    }
}
