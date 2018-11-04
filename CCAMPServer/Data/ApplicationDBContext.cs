using CCAMPServerModel.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CCAMPServer.Data
{
    public class ApplicationDBContext : DbContext
    {
        #region Properties
        public DbSet<SupportUser> SupportUser { get; set; }
        public DbSet<Sponsor> Sponsor { get; set; }
        public DbSet<ContentCreator> ContentCreator { get; set; }
        public DbSet<Campaign> Campaign { get; set; }
        public DbSet<Channel> Channel { get; set; }
        public DbSet<Content> Content { get; set; }
        public DbSet<Deal> Deal { get; set; }
        public DbSet<Advertisement> Advertisement { get; set; }
        public DbSet<Transaction> Transaction { get; set; }
        public DbSet<Token> Token { get; set; }
        public DbSet<TokenRequest> TokenRequest { get; set; }
        #endregion Properties

        #region Constructor
        public ApplicationDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }
        #endregion Constructor
    }

    public class TransactionDBContext : ApplicationDBContext
    {
        #region Constructor
        public TransactionDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }
        #endregion Constructor
    }


    public class TokenDBContext : ApplicationDBContext
    {
        #region Constructor
        public TokenDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }
        #endregion Constructor
    }

    public class TokenRequestDBContext : ApplicationDBContext
    {
        #region Constructor
        public TokenRequestDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }
        #endregion Constructor
    }

}
