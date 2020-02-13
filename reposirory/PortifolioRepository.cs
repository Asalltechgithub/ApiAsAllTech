using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jwt.Models;

namespace jwt.reposirory
{
    public class PortifolioRepository : IPortifolio
    {
        public Portifolio Edit(Portifolio model)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Portifolio> GetAllPortifolio()
        {
            var list = new List<Portifolio>();
            list.Add(
             new Portifolio()
             {
                 Titulo = "App Contador de Pessoas",
                 Id_Portifolio = 1,
                 Imagem = "#",
                 categoria = new Categoria { IdCategoria = 1, Nome_Categoria = "Desiner" },
                 Link = "#"


             }

            );
            list.Add(new Portifolio()
            {
                Titulo = "Instalação de Cameras no Condominio em Iraja",
                Id_Portifolio = 1,
                Imagem = "#",
                categoria = new Categoria { IdCategoria = 2, Nome_Categoria = "Istalação de CFTV" },
                Link = "#"
            });

            return list;
        }

        public Portifolio GetPortifolioById(int Id)
        {
            throw new NotImplementedException();
        }

        public Portifolio Insert(Portifolio model)
        {
            throw new NotImplementedException();
        }

        public Portifolio remove(int Id)
        {
            throw new NotImplementedException();
        }
    }
}
