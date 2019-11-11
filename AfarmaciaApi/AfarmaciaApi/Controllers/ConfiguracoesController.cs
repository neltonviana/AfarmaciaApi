using afarmaciaApi.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace afarmaciaApi.Controllers
{
    [AllowAnonymous]
    public class ConfiguracoesController : Controller
    {

        private const string cryptoKey = "ifarmacia_encripting_key";
        private afarmaciaEntities db = new afarmaciaEntities();
        // The Initialization Vector for the DES encryption routine
        private static readonly byte[] IV =
            new byte[8] { 240, 3, 45, 29, 0, 76, 173, 59 };

        // GET: Configuracao
        public ActionResult Index()
        {
            return View();
        }

        #region //Metodos de auxilio de refinação de dados para não devolver dados irrelevantes
        public List<Usuario> Usuario_refinado(List<Usuario> lista)
        {
            List<Usuario> real = new List<Usuario>();
            Usuario usuario = null;
            foreach (var item in lista)
            {
                usuario = new Usuario
                {
                    id_categoria=item.id_categoria,
                    id_usuario=item.id_usuario,
                    email=item.email,
                    estado=item.estado,
                    senha=null,
                    usuario1=item.usuario1,
                    recover=item.recover,
                    registo=item.registo,
                    link_registo=item.link_registo,
                    origem=item.origem,
                    foto=item.foto,
                    nome=item.nome,
                    situacao=item.situacao,
                    telefone=item.telefone,
                    tipo=item.tipo,
                    apelido=item.apelido,
                    actualizacao=item.actualizacao,
                    Seguro_empresa_usuario=null,
                    Dependente=null,
                    Assinante=null,
                    Carrinho=null,
                    Categoria_usuario=null,
                    Classificacao_farmacia=null,
                    Comentario=null,
                    Ordem=null,
                    Posicao_padrao=null,
                    Receita=null,
                    Seguro_usuario=null,
                    Usuario_logado=null
                };
                real.Add(usuario);
            }
            return real;
        }
        public List<Reserva> Reserva_refinada(List<Reserva> lista)
        {
            List<Reserva> real = new List<Reserva>();
            Reserva entidade = null;
            foreach (var item in lista)
            {
                entidade = new Reserva
                {
                   id_fabricante=item.id_fabricante,
                   id_farmacia=item.id_farmacia,
                   desc_quantidade=item.desc_quantidade,
                   actualizacao=item.actualizacao,
                   dosagem=item.dosagem,
                   id_medicamento=item.id_medicamento,
                   ordem=item.ordem,
                   prescricao=item.prescricao,
                   quantidade=item.quantidade,
                   referencia=item.referencia,
                   registo=item.registo,
                   valor_actual=item.valor_actual,
                   valor_quantidade=item.valor_quantidade,                   
                    estado = item.estado,
                    situacao = item.situacao,
                    Medicamento_farmacia=null,
                    Ordem1=null                    
                };
                real.Add(entidade);
            }
            return real;
        }
        public List<Ordem> Ordem_refinada(List<Ordem> lista)
        {
            List<Ordem> real = new List<Ordem>();
            Ordem entidade = null;
            foreach (var item in lista)
            {
                entidade = new Ordem
                {
                    id_farmacia=item.id_farmacia,
                    id_usuario=item.id_usuario,
                    data=item.data,
                    estado=item.estado,
                    medicamentos=item.medicamentos,
                    observacao=item.observacao,
                    observacao_cancelamento=item.observacao_cancelamento,
                    observacao_emissao=item.observacao_emissao,
                    ordem1=item.ordem1,
                    situacao=item.situacao,
                    valor=item.valor,
                    actualizacao=item.actualizacao,
                    Reserva=Reserva_refinada(db.Reserva.Where(e=>e.ordem.Equals(item.ordem1)).ToList()),
                    Sucursal_farmacia=null,
                    Usuario=null
                };
                real.Add(entidade);
            }
            return real;
        }
        public List<Medicamento_farmacia> Medicemento_farmacia_refinado(List<Medicamento_farmacia> lista )
        {
            List<Medicamento_farmacia> real = new List<Medicamento_farmacia>();
            Medicamento_farmacia entidade = null;
            foreach (var item in lista)
            {
                entidade = new Medicamento_farmacia
                {
                    id_fabricante=item.id_fabricante,
                    id_farmacia=item.id_farmacia,
                    id_medicamento=item.id_medicamento,
                    referencia=item.referencia,
                    estado=item.estado,
                    situacao=item.situacao,
                    preco=item.preco,
                    dosagem=item.dosagem,
                    registo=item.registo,
                    stock=item.stock,
                    apresentacao=item.apresentacao,
                    actualizacao=item.actualizacao,
                    Carrinho=null,
                    Fabricante=null,
                    Medicamento=null,
                    Reserva=null,
                    Sucursal_farmacia=null
                };
                real.Add(entidade);
            }
            return real;
        }
        public List<Medicamento> Medicamento_refinado(List<Medicamento> lista)
        {
            List<Medicamento> real = new List<Medicamento>();
            Medicamento entidade = null;
            foreach (var item in lista)
            {
                entidade = new Medicamento
                {
                    id_grupo=item.id_grupo,
                    id_medicamento=item.id_medicamento,
                    descricao=item.descricao,
                    autorizacao=item.autorizacao,
                    composicao=item.composicao,
                    foto=item.foto,
                    estado=item.estado,
                    registo=item.registo,
                    nome=item.nome,
                    situacao=item.situacao,
                    marca=item.marca,
                    actualizacao=item.actualizacao,
                    Grupo_medicamento=Grupo_medicamento_refinado(db.Grupo_medicamento.Where(e=>e.id_grupo==item.id_grupo).ToList()).FirstOrDefault(),
                    Medicamento_farmacia=null,
                    Receita=null,
                    Seguro_empresa_usuario_medicamento=null,
                    Seguro_usuario_medicamento=null

                    
                };
                real.Add(entidade);
            }
            return real;
        }
        public List<Sucursal_farmacia> Sucursal_farmacia_refinada(List<Sucursal_farmacia> lista)
        {
            List<Sucursal_farmacia> real = new List<Sucursal_farmacia>();
            Sucursal_farmacia entidade = null;
            foreach (var item in lista)
            {
                entidade = new Sucursal_farmacia
                {
                    id_avenida = item.id_avenida,
                    id_distrito = item.id_distrito,
                    id_farmacia = item.id_farmacia,
                    id_sucursal = item.id_sucursal,
                    nome = item.nome,
                    descricao = item.descricao,
                    email = item.email,
                    telefone = item.telefone,
                    data_alvara = item.data_alvara,
                    actualizacao = item.actualizacao,
                    celular = item.celular,
                    estado = item.estado,
                    alvara = item.alvara,
                    fax = item.fax,
                    horario = item.horario,
                    latitude = item.latitude,
                    longitude=item.longitude,
                    lema=item.lema,
                    logo=item.logo,
                    link_localizacao=item.link_localizacao,
                    localizacao=item.localizacao,
                    registo=item.registo,
                    situacao=item.situacao,
                    web_site=item.web_site,
                    Avenida=null,
                    Distrito=null,
                    Classificacao_farmacia=null,
                    Farmacia=null,
                    Medicamento_farmacia=null,
                    Seguro_empresa_usuario_farmacia=null,
                    Ordem=null,
                    Seguro_usuario_farmacia=null
                };
                real.Add(entidade);
            }
            return real;
        }
        public List<Grupo_medicamento> Grupo_medicamento_refinado(List<Grupo_medicamento> lista)
        {
            List<Grupo_medicamento> real = new List<Grupo_medicamento>();
            Grupo_medicamento entidade = null;
            foreach (var item in lista)
            {
                entidade = new Grupo_medicamento
                {
                   id_grupo=item.id_grupo,
                   descricao=item.descricao,
                   estado=item.estado,
                   nome=item.nome,
                   situacao=item.situacao,
                   Medicamento=null
                };
                real.Add(entidade);
            }
            return real;
        }
        public List<Carrinho> Carrinho_refinado(List<Carrinho> lista)
        {
            List<Carrinho> real = new List<Carrinho>();
            Carrinho entidade = null;
            foreach (var item in lista)
            {
                entidade = new Carrinho
                {
                   id_fabricante=item.id_fabricante,
                   id_farmacia=item.id_farmacia,
                   id_medicamento=item.id_medicamento,
                   id_registo=item.id_registo,
                   id_usuario=item.id_usuario,
                   desc_quantidade=item.desc_quantidade,
                   quantidade=item.quantidade,
                   estado=item.estado,
                   situcao=item.situcao,
                   actualizacao=item.actualizacao,
                   observacao=item.observacao,
                   prescricao=item.prescricao,
                   referencia=item.referencia,
                   registo=item.registo,
                   valor_quantidade=item.valor_quantidade,
                   Medicamento_farmacia=null,
                   Usuario=null
                };
                real.Add(entidade);
            }
            return real;
        }
        public List<Autorizacao> Autorizacao_refinada(long id_medicamento, string retorno)
        {
            var med = db.Medicamento_farmacia.Where(e => e.id_medicamento==id_medicamento).ToList();
            List<Autorizacao> lista = new List<Autorizacao>();
            Autorizacao med_form = new Autorizacao();
            if (retorno.Contains("orma"))
            {
                foreach (var item in med)
                {
                    med_form = new Autorizacao();
                    if (lista.Where(e => e.designacao.Contains(item.apresentacao)).Count() == 0)
                    {
                        med_form.designacao = item.apresentacao;
                        med_form.tipo = retorno;
                        med_form.id = item.id_medicamento;
                        lista.Add(med_form);
                    }
                }
            }
            else
            {
                foreach (var item in med)
                {
                    med_form = new Autorizacao();
                    if (lista.Where(e => e.designacao.Contains(item.dosagem)).Count() == 0)
                    {
                        med_form.designacao = item.dosagem;
                        med_form.tipo = retorno;
                        med_form.id = item.id_medicamento;
                        lista.Add(med_form);
                    }
                }
            }
           return lista.OrderBy(e=>e.designacao).ToList();
        }

        public List<Receita> Receita_refinada(List<Receita> lista)
        {
            List<Receita> real = new List<Receita>();
            Receita entidade = null;
            foreach (var item in lista)
            {
                entidade = new Receita
                {
                    id_medicamento=item.id_medicamento,
                    id_registo=item.id_registo,
                    id_usuario=item.id_usuario,
                    estado=item.estado,
                    actualizacao=item.actualizacao,
                    composicao=item.composicao,
                    data=item.data,
                    forma=item.forma,
                    quantidade=item.quantidade,
                    registo=item.registo,
                    situacao=item.situacao,
                    Usuario=null,
                    Medicamento=null
                };
                real.Add(entidade);
            }
            return real;
        }

        public List<Pesquisas> Procura_refinada(List<Pesquisas> lista)
        {
            List<Pesquisas> real = new List<Pesquisas>();
            Pesquisas entidade = null;
            foreach (var item in lista)
            {
                entidade = new Pesquisas
                {
                   id=item.id,
                   id_farmacia=item.id_farmacia,
                   raio=item.raio,
                   id_fabricante=item.id_fabricante,
                   referencia=item.referencia,
                   ditancia_farmacia=item.ditancia_farmacia,
                   total_encontrado=item.total_encontrado,
                   total_receita=item.total_receita,
                   id_usuario=item.id_usuario,
                   data=item.data,
                   dosagem=item.dosagem,
                   forma=item.forma,
                   id_medicamento=item.id_medicamento,
                   id_registo=item.id_registo,
                   preco_unitario=item.preco_unitario,
                   quantidade=item.quantidade,
                   total_valor=item.total_valor
                };
                real.Add(entidade);
            }
            return real;
        }

        public List<Receita_real> Receita_real_refinada(List<Receita_real> lista)
        {
            List<Receita_real> real = new List<Receita_real>();
            List<Medicamento_farmacia> list = new List<Medicamento_farmacia>();
            List<Receita> list2 = new List<Receita>();
            Receita_real entidade = null;
            foreach (var item in lista)
            {
                list.Add(item.medicamento);
                list2.Add(item.receita);
                entidade = new Receita_real
                {
                    medicamento=Medicemento_farmacia_refinado(list).FirstOrDefault(),
                    receita=Receita_refinada(list2).FirstOrDefault(),
                    total=item.total
                };
                real.Add(entidade);
            }
            return real;
        }
        #endregion

        #region//Metodos de auxilio de consulta
        public List<Pesquisas> Procura_farmacias( string local, string data, long id_usuario, float? raio)
        {
            if (raio == null)
            {
                raio = 5;
            }
           
            //TempData["pesquisa"] = (List<Medicamento_farmacia>)lista;
            //Inicializacao da magia de pesquisa;

            List<Sucursal_farmacia> farmacias = new List<Sucursal_farmacia>();
            List<Receita> receitas = new List<Receita>();
            List<Medicamento_farmacia> resultado = new List<Medicamento_farmacia>();
            List<Medicamento_farmacia> resultado2 = new List<Medicamento_farmacia>();
            List<Pesquisas> resumo = new List<Pesquisas>();
            Pesquisas consulta = new Pesquisas();
            Medicamento_farmacia med_farm = new Medicamento_farmacia();
            Receita_real real = new Receita_real();
            List<Reserva> reserva = new List<Reserva>();
            Reserva reserva_melhor = new Reserva();
            List<Medicamento_receita> lista2 = new List<Medicamento_receita>();
            Medicamento_receita rm = new Medicamento_receita();
            long contador = 1;
            decimal distancia2 = 0;
            //Fim da magia de pesquisa

            //Magia de pesquisa

            if (data != null)
            {
                Posicao_padrao posicao = Get_posicao(id_usuario);
                resultado = Farmacias_proximas((int)raio, id_usuario);
                if (resultado != null)
                {
                    receitas = Minha_receita(id_usuario);
                    foreach (var item in resultado)
                    {
                        int encontrado = 0;
                        if (farmacias.Where(e => e.id_sucursal == item.id_farmacia).Count() == 0 && receitas.Where(a => a.id_medicamento == item.id_medicamento).Count()> 0)
                        {
                            Receita ras = receitas.Where(a => a.id_medicamento == item.id_medicamento).FirstOrDefault();
                           
                                //total = (decimal)(ras.quantidade * item.preco);
                                encontrado++;

                                distancia2 = (decimal)(Math.Round(Calcula_distancia((float)item.Sucursal_farmacia.latitude, (float)item.Sucursal_farmacia.longitude, 'K', posicao), 1));
                                consulta = new Pesquisas
                                {
                                    id = contador,
                                    id_fabricante=item.id_fabricante,
                                    referencia=item.referencia,
                                    id_farmacia = item.id_farmacia,
                                    raio = raio,
                                    ditancia_farmacia = distancia2,
                                    total_encontrado = encontrado,
                                    id_medicamento = ras.id_medicamento,
                                    data = ras.data,
                                    dosagem = ras.composicao,
                                    forma = ras.forma,
                                    id_registo = ras.id_registo,
                                    id_usuario = ras.id_usuario,
                                    preco_unitario = item.preco,
                                    quantidade = ras.quantidade,
                                    total_receita = receitas.Count(),
                                    total_valor = (decimal)(ras.quantidade * item.preco)
                                };
                                contador++;
                                resumo.Add(consulta);
                                //farmacias.Add(item.Sucursal_farmacia);
                                encontrado = 0;
                            }

                    }

                    //Historico de pesquisas sobre os medicamentos da receita
                    /*
                      foreach (var item in receitas)
                    {
                        try
                        {
                            reserva_melhor = reserva.Where(e => e.dosagem.Contains(item.composicao) && e.desc_quantidade.Contains(item.forma) && e.id_medicamento == item.id_medicamento).OrderBy(e => e.valor_actual).First();
                            if (reserva_melhor != null)
                            {
                               // var distancia2 = Math.Round(Calcula_distancia((float)reserva_melhor.Medicamento_farmacia.Sucursal_farmacia.latitude, (float)reserva_melhor.Medicamento_farmacia.Sucursal_farmacia.longitude, 'K', posicao), 1);
                                med_farm = resultado2.Where(e => e.id_medicamento == item.id_medicamento && e.apresentacao.Equals(item.forma) && e.dosagem.Equals(item.composicao) && e.id_farmacia == reserva_melhor.id_farmacia && e.id_fabricante == reserva_melhor.id_fabricante && e.preco == reserva_melhor.valor_actual).FirstOrDefault();
                                rm = new Medicamento_receita
                                {
                                    consulta = med_farm,
                                    distancia = (float)distancia2
                                };
                                lista2.Add(rm);

                            }

                        }
                        catch { }
                    }
                 */
                    //Envio do resultado para a pagina de disponibilização do resultado

                }

            }
            //Fim da magia de pesquisa

            return resumo;
        }
        //Metodo que retorna as farmacias mais proximas do usuario
        public List<Medicamento_farmacia> Farmacias_proximas(float raio, long? id_usuario)
        {
            List<Medicamento_farmacia> proximas = new List<Medicamento_farmacia>();
            float distancia = 0;
            try
            {
                var posicao_inicial = Get_posicao(id_usuario);
                var lista = db.Medicamento_farmacia.Where(e => e.estado == 1 && e.situacao == 1).ToList();
                foreach (var item in lista.OrderBy(e=>e.preco))
                {
                    distancia = (float)(Math.Round(Calcula_distancia((float)item.Sucursal_farmacia.latitude, (float)item.Sucursal_farmacia.longitude, 'K', posicao_inicial), 3));
                    if (proximas.Where(e=>e.id_farmacia==item.id_farmacia && e.id_medicamento==item.id_medicamento).Count()==0)
                    {
                        if (distancia <= raio)
                        {
                            proximas.Add(item);
                        }
                    }
                    distancia = 0;
                }
            }
            catch { }

            return proximas;
        }

        
        // Metodo de receitas activas do usuario
        public List<Receita> Minha_receita(long id_usuario)
        {
            var data = DateTime.Now;
            string registo = data.Year + "" + data.Month + "" + data.Day;

            List<Receita> receita = db.Receita.Where(e => e.id_usuario==id_usuario && e.situacao == 1 && e.data.Equals(registo)).ToList();

            return receita;
        }
        //Metodo certo para o calculo de distancias via latitude e longitude do tipo 25.551252,-14.255645
        public double Calcula_distancia(float lati2, float long2, char unit, Posicao_padrao posicao)
        {
            //var posicao = Get_posicao(id_usuario);
            double deg2radMultiplier = 3.14159265358979323846 / 180;

            double lat1 = -25.954161 * deg2radMultiplier;
            double lat2 = (lati2) * deg2radMultiplier;
            double lon1 = 32.580377 * deg2radMultiplier;
            double lon2 = (long2) * deg2radMultiplier;

            double radius = 6378.137; // earth mean radius defined by WGS84
            double dlon = lon2 - lon1;
            double distance = Math.Acos(Math.Sin(lat1) * Math.Sin(lat2) + Math.Cos(lat1) * Math.Cos(lat2) * Math.Cos(dlon)) * radius;

            if (unit == 'K')
            {
                return (distance);
            }
            else if (unit == 'M')
            {
                return (distance * 0.621371192);
            }
            else if (unit == 'N')
            {
                return (distance * 0.539956803);
            }
            else
            {
                return 0;
            }
        }


        //METODO DE CALCULO DE DISTANCIA
        public double Calcular_distancia(float destino_lat, float destino_lng)
        {
            var posicao = Get_posicao(null);
            double x1 = -25.954161;
            double x2 = (destino_lat);
            double y1 = 32.580377;
            double y2 = (destino_lng);

            // Distancia entre os 2 pontos no plano cartesiano ( pitagoras )
            //double distancia = System.Math.Sqrt( System.Math.Pow( (x2 - x1), 2 ) + System.Math.Pow( (y2 - y1), 2 ) );

            // ARCO AB = c 
            double c = 90 - (y2);

            // ARCO AC = b 
            double b = 90 - (y1);

            // Arco ABC = a 
            // Diferença das longitudes: 
            double a = x2 - x1;

            // Formula: cos(a) = cos(b) * cos(c) + sen(b)* sen(c) * cos(A) 
            double cos_a = Math.Cos(b) * Math.Cos(c) + Math.Sin(c) * Math.Sin(b) * Math.Cos(a);

            double arc_cos = Math.Acos(cos_a);

            // 2 * pi * Raio da Terra = 6,28 * 6.371 = 40.030 Km 
            // 360 graus = 40.030 Km 
            // 3,2169287 = x 
            // x = (40.030 * 3,2169287)/360 = 357,68 Km 

            double distancia = (40030 * arc_cos) / 360;

            return distancia;
        }

        //METODO QUE DISPONIBILIZA A POSICAO DO INDIVIDUO
        public Posicao_padrao Get_posicao(long? id_usuario)
        {
            Posicao_padrao posicao = new Posicao_padrao();
            try
            {
                if (id_usuario != null)
                {
                    posicao = db.Posicao_padrao.Where(e => e.estado == 1 && e.situacao == 1 && e.id_usuario == id_usuario).FirstOrDefault();
                }
                else
                {
                    posicao = db.Posicao_padrao.Where(e => e.estado == 1 && e.situacao == 1 && e.id_usuario==null).FirstOrDefault();
                }
            }
            catch { }

            return posicao;
        }
        #endregion

        #region//Metodos de auxilio de registo de dados
        public  Ordem Adicionar_ordem(Ordem enviada,string local,int raio, string data)
        {
            var data_hoje = DateTime.Now;
            Ordem ordem = new Ordem();
            
            Reserva reserva = new Reserva();
            Receita receita = new Receita();
            List<Receita> lista_receita = new List<Receita>();
            List<Reserva> lista_reserva = new List<Reserva>();
            ordem.actualizacao = data_hoje;
            string id_ordem = ""+data + enviada.id_usuario + enviada.id_farmacia;
            try
            {
                var encontrar = db.Ordem.Where(e => e.id_farmacia == enviada.id_farmacia && e.id_usuario == enviada.id_usuario && e.data.Value.Year == data_hoje.Year && e.data.Value.Month == data_hoje.Month && e.data.Value.Day == data_hoje.Day).Count();
                id_ordem = encontrar + "" + id_ordem;
            }
            catch { }
            
            var lista = Procura_farmacias(local, data, (long)enviada.id_usuario, raio).Where(e=>e.id_farmacia==enviada.id_farmacia).ToList();
            if (lista.Count()!=0)
            {
                ordem = new Ordem
                {
                    id_farmacia=enviada.id_farmacia,
                    id_usuario=enviada.id_usuario,
                    data=data_hoje,
                    actualizacao=data_hoje,
                    estado=1,
                    situacao=1,
                    observacao=enviada.observacao,
                    observacao_cancelamento=enviada.observacao_cancelamento,
                    observacao_emissao=enviada.observacao_emissao,
                    medicamentos= lista.Count(),
                    valor = lista.Sum(e => e.total_valor),
                    ordem1=id_ordem,
                };
                db.Ordem.Add(ordem);
                db.SaveChanges();
                foreach (var item in lista)
                {
                    var encontrou = db.Medicamento_farmacia.Find(item.id_medicamento,item.id_farmacia,item.id_fabricante,item.referencia);

                    if (encontrou!=null)
                    {
                        reserva = new Reserva
                        {
                            id_medicamento = item.id_medicamento,
                            id_fabricante = encontrou.id_fabricante,
                            id_farmacia = encontrou.id_farmacia,
                            desc_quantidade = item.forma,
                            dosagem = item.dosagem,
                            ordem = ordem.ordem1,
                            estado = 1,
                            prescricao="",
                            quantidade=item.quantidade,
                            referencia=encontrou.referencia,
                            valor_actual=encontrou.preco,
                            valor_quantidade=item.quantidade*encontrou.preco,
                            registo=data_hoje,
                            actualizacao=data_hoje,
                            situacao=1
                        };
                        receita = new Receita();
                            receita=db.Receita.Find(item.id_registo, item.id_medicamento, item.id_usuario, item.data);

                        if (receita!=null)
                        {
                            receita.situacao = 0;
                            receita.estado = 1;
                            receita.actualizacao = DateTime.Now;
                            lista_receita.Add(receita);
                        }
                        lista_reserva.Add(reserva);
                    }

                }
                try
                {
                    db.Reserva.AddRange(lista_reserva);
                    db.SaveChanges();
                    foreach (var rc in lista_receita)
                    {
                        Retirar_receita_ciclo(rc.id_medicamento, rc.data, rc.id_registo, rc.id_usuario);
                    }
                }
                catch(Exception)
                {
                    //return null;
                    db.Ordem.Remove(ordem);
                    db.SaveChanges();
                }
            }
            return ordem;
        }
        public string Confirmar_venda(string ordem, long id_usuario, string observacao)
        {
            Usuario usuario = new Usuario();
            long total = 0;
            List<Medicamento_farmacia> medicamento = db.Medicamento_farmacia.Where(e => e.estado == 1).ToList();
            var data = DateTime.Now;
            
            if (observacao == null)
            {
                observacao = "Sem observação!";
            }
            Ordem actualiza = db.Ordem.Find(ordem);
            if (actualiza == null)
            {
                return "Não foi possível efectuar a venda";
            }
            try
            {
                var reservas = db.Reserva.Where(e => e.ordem.Equals(ordem));
                usuario = db.Usuario.Find(id_usuario);
                foreach (var item in reservas)
                {
                    //Encontra o medicamento na farmacia e decrementar a quantidade e actualizar a data
                    var medicamentos = medicamento.Where(e => e.id_medicamento == item.id_medicamento && e.id_farmacia == item.id_farmacia && e.id_fabricante == item.id_fabricante && e.referencia == item.referencia).FirstOrDefault();
                    medicamentos.actualizacao = data;
                    medicamentos.stock = medicamentos.stock - item.quantidade;
                    db.Entry(medicamentos).State = EntityState.Modified;

                    //contagem
                    total = (long)(total + item.quantidade);
                    //Actualizar a reserva
                    item.actualizacao = data;
                    item.situacao = 0;
                    item.prescricao = item.prescricao + ".Actualizado por confirmacao da venda";
                    db.Entry(item).State = EntityState.Modified;

                }
                //Inicio de actualizacao das vendas
                actualiza.actualizacao = data;
                actualiza.situacao = 0;
                actualiza.observacao_emissao = "Ordem actualizada por " + usuario.nome + " " + usuario.apelido + ". " + observacao;
                db.Entry(actualiza).State = EntityState.Modified;
                db.SaveChanges();

                //FIM DA ACTUALIZACAO
            }
            catch { }

            return "Venda efectuada com sucesso com abate de " + total + " medicamentos do stock! Se desconhece esta acção pode reverte-la em Histório de compras no seu perfil";
        }

        public ActionResult Retirar_receita_ciclo(long id_medicamento, string data, long id_registo, long id_usuario)
        {
            // string mensagem = "Não foi possível retirar o medicamento, tente novamente";
            //Get Default user
            try
            {
                //id_usuario = User_by_email(User.Identity.GetUserEmail()).id_usuario;
                Receita carrinho = db.Receita.Where(e => e.id_medicamento == id_medicamento && e.id_registo == id_registo && e.data.Equals(data) && e.id_usuario == id_usuario).FirstOrDefault();
                if (carrinho != null)
                {
                    // mensagem = "O medicamento ja foi retirado da receita! Obrigado...";
                    carrinho.situacao = 0;
                    carrinho.estado = 1;
                    carrinho.actualizacao = DateTime.Now;
                    db.Entry(carrinho).State = EntityState.Modified;
                    db.SaveChanges();

                }
            }
            catch { }
            //Seccao de preenchimento do modelo

            return null;
        }

        public Receita Adicionar_receita(long id_medicamento, string dosagem, int? quantidade, string forma, long id_usuario)
        {
            var data = DateTime.Now;
            long id = 0;
            Receita receita = null;
            string registo = data.Year + "" + data.Month + "" + data.Day;

            //Obter o usuario logado
            try
            {
                //id_usuario = User_by_email(User.Identity.GetUserEmail()).id_usuario;
            }
            catch { }
            //Procurar medicamento pelo nome
           
            
            //Teste do usuario logado
            if (id_usuario != -1 && id_medicamento != -1 && quantidade != null)
            {

                try
                {
                    var existe = db.Receita.Where(e => e.id_usuario == id_usuario && e.id_medicamento==id_medicamento).ToList();
                    if (existe.Count() != 0)
                    {
                        try
                        {
                            Receita encontrada = existe.Where(e => e.data.Equals(registo) && e.composicao.Equals(dosagem)).FirstOrDefault();
                            if (encontrada != null)
                            {
                                encontrada.situacao = -1;
                                encontrada.actualizacao = data;
                                db.Entry(encontrada).State = EntityState.Modified;
                                db.SaveChanges();
                                id = existe.Max(e => e.id_registo) + 1;
                            }
                            else
                            {
                                id = existe.Max(e => e.id_registo) + 1;
                            }
                            
                        }
                        catch
                        {
                            //id = existe.Max(e => e.id_registo) + 1;
                        }
                    }

                }
                catch { }

               receita = new Receita()
                {
                    id_registo = id,
                    id_usuario = id_usuario,
                    id_medicamento = id_medicamento,
                    data = registo,
                    actualizacao = data,
                    registo = data,
                    estado = 1,
                    composicao = dosagem,
                    quantidade = quantidade,
                    situacao = 1,
                    forma = forma
                };
                try
                {
                    db.Receita.Add(receita);
                    db.SaveChanges();
                    return receita;

                    // return RedirectToAction("Index", new { alerta = "O " + medicamento+" foi adicionado a receita com a quantidade "+quantidade });

                }
                catch
                {
                    
                    return receita;
                    //RedirectToAction("Index", new { mensagem = "Não foi possível adicionar a receita" + medicamento });
                }

            }
            else
            {
                return receita;
                //return RedirectToAction("Index", new { mensagem = "Deve entrar no sistema para ter acesso a receitas (Canto superior Direito)" });
            }


        }
        #endregion

        #region//Metodos de encriptação de dados
        public string Desmensagiar(string s)
        {
            if (s == null || s.Length == 0) return string.Empty;

            string result = string.Empty;

            try
            {
                byte[] buffer = Convert.FromBase64String(s);

                TripleDESCryptoServiceProvider des =
                    new TripleDESCryptoServiceProvider();

                MD5CryptoServiceProvider MD5 =
                    new MD5CryptoServiceProvider();

                des.Key =
                    MD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(cryptoKey));

                des.IV = IV;

                result = Encoding.ASCII.GetString(
                    des.CreateDecryptor().TransformFinalBlock(
                    buffer, 0, buffer.Length));
            }
            catch
            {
                throw;
            }

            return result;
        }

        [AllowAnonymous]
        public string Mensagiar(string s)
        {
            if (s == null || s.Length == 0) return string.Empty;

            string result = string.Empty;

            try
            {
                byte[] buffer = Encoding.ASCII.GetBytes(s);

                TripleDESCryptoServiceProvider des =
                    new TripleDESCryptoServiceProvider();

                MD5CryptoServiceProvider MD5 =
                    new MD5CryptoServiceProvider();

                des.Key =
                    MD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(cryptoKey));

                des.IV = IV;
                result = Convert.ToBase64String(
                    des.CreateEncryptor().TransformFinalBlock(
                        buffer, 0, buffer.Length));
            }
            catch
            {
                throw;
            }

            return result;
        }

        #endregion
    }

}