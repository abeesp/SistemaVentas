using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;
using CapaEntidad;

namespace CapaDatos
{
    public class CD_Usuario
    {

        public List<Usuario> Listar()
        {
            List<Usuario> lista = new List<Usuario>();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {

                try
                {

                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select u.IdUsuario,u.Documento,u.NombreCompleto,u.Correo,u.Clave,u.Estado,r.IdRol,r.Descripcion from usuario u");
                    query.AppendLine("inner join rol r on r.IdRol = u.IdRol");


                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {

                        while (dr.Read())
                        {

                            lista.Add(new Usuario()
                            {
                                IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                                Documento = dr["Documento"].ToString(),
                                NombreCompleto = dr["NombreCompleto"].ToString(),
                                Correo = dr["Correo"].ToString(),
                                Clave = dr["Clave"].ToString(),
                                Estado = Convert.ToBoolean(dr["Estado"]),
                                oRol = new Rol() { IdRol = Convert.ToInt32(dr["IdRol"]), Descripcion = dr["Descripcion"].ToString() }
                            });

                        }

                    }


                }
                catch (Exception ex)
                {

                    lista = new List<Usuario>();
                }
            }

            return lista;

        }

        public int Registrar(Usuario obj, out string Mensaje)
        {
            int idusuariogenerado = 0;
            Mensaje = string.Empty;


            try
            {

                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {



                    SqlCommand cmd = new SqlCommand("SP_REGISTRARUSUARIO", oconexion);


                    cmd.Parameters.AddWithValue("Documento", obj.Documento);
                    cmd.Parameters.AddWithValue("NombreCompleto", obj.NombreCompleto);
                    cmd.Parameters.AddWithValue("Correo", obj.Correo);
                    cmd.Parameters.AddWithValue("Clave", obj.Clave);
                    cmd.Parameters.AddWithValue("IdRol", obj.oRol.IdRol);
                    cmd.Parameters.AddWithValue("Estado", obj.Estado);
                    cmd.Parameters.Add("IdUsuarioResultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();

                    idusuariogenerado = Convert.ToInt32(cmd.Parameters["IdUsuarioResultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();

                }

            }
            catch (Exception ex)
            {
                idusuariogenerado = 0;
                Mensaje = ex.Message;
            }



            return idusuariogenerado;
        }
        public bool Editar(Usuario obj, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;


            try
            {

                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {

                    SqlCommand cmd = new SqlCommand("SP_EDITARUSUARIO", oconexion);
                    cmd.Parameters.AddWithValue("IdUsuario", obj.IdUsuario);
                    cmd.Parameters.AddWithValue("Documento", obj.Documento);
                    cmd.Parameters.AddWithValue("NombreCompleto", obj.NombreCompleto);
                    cmd.Parameters.AddWithValue("Correo", obj.Correo);
                    cmd.Parameters.AddWithValue("Clave", obj.Clave);
                    cmd.Parameters.AddWithValue("IdRol", obj.oRol.IdRol);
                    cmd.Parameters.AddWithValue("Estado", obj.Estado);
                    cmd.Parameters.Add("Respuesta", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["Respuesta"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();

                }

            }
            catch (Exception ex)
            {
                respuesta = false;
                Mensaje = ex.Message;
            }



            return respuesta;
        }


        public bool Eliminar(Usuario obj, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;


            try
            {

                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {


                    SqlCommand cmd = new SqlCommand("SP_ELIMINARUSUARIO", oconexion);
                    cmd.Parameters.AddWithValue("IdUsuario", obj.IdUsuario);
                    cmd.Parameters.Add("Respuesta", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["Respuesta"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();

                }

            }
            catch (Exception ex)
            {
                respuesta = false;
                Mensaje = ex.Message;
            }

            return respuesta;
        }


        #region NuevoCodigo

        //private void CrearParametros(ref SqlCommand comando, Usuario entidad)
        //{
        //    if (entidad.Usuario != null)
        //        comando.Parameters.Add(new SqlParameter("USUARIO", entidad.Usuario));
        //    else
        //        comando.Parameters.Add(new SqlParameter("USUARIO", DBNull.Value));
        //    if (entidad.Clave != null)
        //        comando.Parameters.Add(new SqlParameter("CLAVE", entidad.Clave));
        //    else
        //        comando.Parameters.Add(new SqlParameter("CLAVE", DBNull.Value));
        //    if (entidad.Tipo != null)
        //        comando.Parameters.Add(new SqlParameter("TIPO", entidad.Tipo));
        //    else
        //        comando.Parameters.Add(new SqlParameter("TIPO", DBNull.Value));
        //    if (entidad.Marca != null)
        //        comando.Parameters.Add(new SqlParameter("MARCA", entidad.Marca));
        //    else
        //        comando.Parameters.Add(new SqlParameter("MARCA", DBNull.Value));
        //    if (entidad.Modelo != null)
        //        comando.Parameters.Add(new SqlParameter("MODELO", entidad.Modelo));
        //    else
        //        comando.Parameters.Add(new SqlParameter("MODELO", DBNull.Value));
        //    if (entidad.Anio != null)
        //        comando.Parameters.Add(new SqlParameter("ANIO", entidad.Anio));
        //    else
        //        comando.Parameters.Add(new SqlParameter("ANIO", DBNull.Value));
        //    if (entidad.Valor != null)
        //        comando.Parameters.Add(new SqlParameter("VALOR", entidad.Valor));
        //    else
        //        comando.Parameters.Add(new SqlParameter("VALOR", DBNull.Value));
        //    if (entidad.Formaadquisicion != null)
        //        comando.Parameters.Add(new SqlParameter("FORMAADQUISICION", entidad.Formaadquisicion));
        //    else
        //        comando.Parameters.Add(new SqlParameter("FORMAADQUISICION", DBNull.Value));
        //    if (entidad.Propietario != null)
        //        comando.Parameters.Add(new SqlParameter("PROPIETARIO", entidad.Propietario));
        //    else
        //        comando.Parameters.Add(new SqlParameter("PROPIETARIO", DBNull.Value));
        //    if (entidad.Estatus != null)
        //        comando.Parameters.Add(new SqlParameter("ESTATUS", entidad.Estatus));
        //    else
        //        comando.Parameters.Add(new SqlParameter("ESTATUS", DBNull.Value));
        //    if (entidad.OtroVehiculo != null)
        //        comando.Parameters.Add(new SqlParameter("OTROVEHICULO", entidad.OtroVehiculo));
        //    else
        //        comando.Parameters.Add(new SqlParameter("OTROVEHICULO", DBNull.Value));
        //    if (entidad.AnioDeclaracion != null)
        //        comando.Parameters.Add(new SqlParameter("ANIODECLARACION", entidad.AnioDeclaracion));
        //    else
        //        comando.Parameters.Add(new SqlParameter("ANIODECLARACION", DBNull.Value));
        //    if (entidad.TipoDeclaracion != null)
        //        comando.Parameters.Add(new SqlParameter("TIPODECLARACION", entidad.TipoDeclaracion));
        //    else
        //        comando.Parameters.Add(new SqlParameter("TIPODECLARACION", DBNull.Value));
        //    if (entidad.TipoMovimiento != null && entidad.TipoMovimiento != -1)
        //        comando.Parameters.Add(new SqlParameter("TIPOMOVIMIENTO", entidad.TipoMovimiento));
        //    else
        //        comando.Parameters.Add(new SqlParameter("TIPOMOVIMIENTO", DBNull.Value));
        //    if (entidad.MontoMovimiento != null && entidad.MontoMovimiento != -1)
        //        comando.Parameters.Add(new SqlParameter("MONTOMOVIMIENTO", entidad.MontoMovimiento));
        //    else
        //        comando.Parameters.Add(new SqlParameter("MONTOMOVIMIENTO", DBNull.Value));
        //    if (entidad.FechaMovimiento != null)
        //        comando.Parameters.Add(new SqlParameter("FECHAMOVIMIENTO", entidad.FechaMovimiento));
        //    else
        //        comando.Parameters.Add(new SqlParameter("FECHAMOVIMIENTO", DBNull.Value));
        //    if (entidad.ComentarioMovimiento != null)
        //        comando.Parameters.Add(new SqlParameter("COMENTARIOMOVIMIENTO", entidad.ComentarioMovimiento));
        //    else
        //        comando.Parameters.Add(new SqlParameter("COMENTARIOMOVIMIENTO", DBNull.Value));
        //}

        //public int Consecutivo(FiltroVehiculo filtro)
        //{
        //    SqlConnection conexion = Conexion.ObtenerConexionDeclaracion();
        //    try
        //    {
        //        StringBuilder sentencia = new StringBuilder("SELECT MAX(NVL(CLAVE,0)) FROM VEHICULOS ");
        //        sentencia.AppendLine(" WHERE USUARIO = :USUARIO  ");
        //        SqlCommand comando = Conexion.ObtenerComando(conexion, sentencia.ToString());
        //        comando.Parameters.Add(new SqlParameter("USUARIO", filtro.Usuario));
        //        object resultado = comando.ExecuteScalar();
        //        if (resultado != null && resultado.GetType() != typeof(DBNull))
        //        {
        //            int max = Convert.ToInt32(resultado);
        //            return ++max;
        //        }
        //        else
        //        {
        //            return 1;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //    finally
        //    {
        //        conexion.Close();
        //        SqlConnection.ClearPool(conexion);
        //    }
        //}

        //public bool Insertar(Usuario entidad)
        //{


        //    entidad.Clave = Consecutivo(fVehiculo);

        //    //entidad.AnioDeclaracion = DateTime.Now.Year.ToString();

        //    SqlConnection conexion = Conexion.ObtenerConexionDeclaracion();
        //    SqlTransaction transaccion = conexion.BeginTransaction();
        //    try
        //    {
        //        StringBuilder sentencia = new StringBuilder(" INSERT INTO USUARIO ");
        //        sentencia.AppendLine(" (IDUSUARIO, DOCUMENTO, NOMBRECOMPLETO, CORREO, CLAVE, IDROL, ESTADO, FECHAREGISTRO, ) ");
        //        sentencia.AppendLine(" VALUES ( @IDUSUARIO, @DOCUMENTO, @NOMBRECOMPLETO, @CORREO, @CLAVE, @IDROL, @ESTADO, @FECHAREGISTRO,  )  ");
        //        SqlCommand comando = Conexion.ObtenerComando(conexion, sentencia.ToString(), transaccion);

        //        comando.Parameters.Add(new SqlParameter("IDUSUARIO", entidad.IdUsuario));
        //        comando.Parameters.Add(new SqlParameter("DOCUMENTO", entidad.IdUsuario));
        //        comando.Parameters.Add(new SqlParameter("NOMBRECOMPLETO", entidad.IdUsuario));
        //        comando.Parameters.Add(new SqlParameter("CORREO", entidad.IdUsuario));
        //        comando.Parameters.Add(new SqlParameter("CLAVE", entidad.IdUsuario));
        //        comando.Parameters.Add(new SqlParameter("IDROL", entidad.IdUsuario));
        //        comando.Parameters.Add(new SqlParameter("ESTADO", entidad.IdUsuario));
        //        comando.Parameters.Add(new SqlParameter("FECHAREGISTRO", entidad.IdUsuario));


        //        CrearParametros(ref comando, entidad);
        //        comando.ExecuteNonQuery();

        //        if (entidad.InsertarEstatus == "SI")
        //        {
        //            sentencia = new StringBuilder(@" SELECT USUARIO FROM ESTATUSCAPTURAS WHERE USUARIO = :USUARIO AND APARTADO = :APARTADO AND ESTATUS= :ESTATUS AND TIPO = :TIPODEC AND ANIO = :ANIO ");
        //            comando = Conexion.ObtenerComando(conexion, sentencia.ToString(), transaccion);
        //            comando.Parameters.Add(new SqlParameter("USUARIO", entidad.Usuario));
        //            comando.Parameters.Add(new SqlParameter("APARTADO", entidad.Apartado));
        //            comando.Parameters.Add(new SqlParameter("ESTATUS", "E"));
        //            comando.Parameters.Add(new SqlParameter("TIPODEC", entidad.TipoDeclaracion));
        //            comando.Parameters.Add(new SqlParameter("ANIO", entidad.AnioDeclaracion));

        //            SqlDataReader lector = comando.ExecuteReader();
        //            string _usuario = null;
        //            while (lector.Read())
        //            {
        //                _usuario = !(lector["USUARIO"] is DBNull) ? lector["USUARIO"].ToString() : string.Empty;
        //            }
        //            lector.Close();
        //            if (_usuario == null)
        //            {
        //                EstatusCaptura entidadEstatus = new EstatusCaptura();
        //                entidadEstatus.Usuario = entidad.Usuario;
        //                entidadEstatus.Estatus = "E";
        //                entidadEstatus.Apartado = entidad.Apartado;
        //                entidadEstatus.Tipo = entidad.TipoDeclaracion;
        //                entidadEstatus.Anio = entidad.AnioDeclaracion;
        //                AccesoDatos.ServiciosEstatusCaptura estatusCaptura = new AccesoDatos.ServiciosEstatusCaptura();
        //                estatusCaptura.Insertar(entidadEstatus, conexion, transaccion);
        //            }
        //        }
        //        transaccion.Commit();
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        transaccion.Rollback();
        //        throw new Exception(ex.Message);
        //    }
        //    finally
        //    {
        //        conexion.Close();
        //        SqlConnection.ClearPool(conexion);
        //    }
        //}

        //public bool Modificar(Usuario entidad)
        //{
        //    SqlConnection conexion = Conexion.ObtenerConexionDeclaracion();
        //    SqlTransaction transaccion = conexion.BeginTransaction();
        //    StringBuilder sentencia = new StringBuilder();
        //    SqlCommand comando = Conexion.ObtenerComando(conexion, sentencia.ToString(), transaccion);
        //    try
        //    {
        //        if ((entidad.TipoDeclaracion == "M" || entidad.TipoDeclaracion == "C") && (entidad.AnioDeclaracionAnterior < Convert.ToInt32(entidad.AnioDeclaracion)))
        //        {
        //            sentencia = new StringBuilder(@" SELECT MARCA FROM VEHICULOS WHERE USUARIO = :USUARIO AND TIPODECLARACION = :TIPODECANT AND CLAVE = :CLAVE ");
        //            comando = Conexion.ObtenerComando(conexion, sentencia.ToString(), transaccion);
        //            comando.Parameters.Add(new SqlParameter("USUARIO", entidad.Usuario));
        //            comando.Parameters.Add(new SqlParameter("TIPODECANT", entidad.TipoDeclaracionAnterior)); //Tal vez en anual necesite agregar el tipo "M" para verificar si existe registro
        //            comando.Parameters.Add(new SqlParameter("CLAVE", entidad.Clave));
        //            //comando.Parameters.Add(new SqlParameter("ANIO", entidad.AnioDeclaracion));

        //            SqlDataReader lector = comando.ExecuteReader();
        //            string _marca = null;
        //            while (lector.Read())
        //            {
        //                _marca = !(lector["MARCA"] is DBNull) ? lector["MARCA"].ToString() : string.Empty;
        //            }
        //            lector.Close();
        //            if (_marca != null && (entidad.AnioDeclaracionAnterior < Convert.ToInt32(entidad.AnioDeclaracion)))
        //            {
        //                FiltroVehiculo fVehiculo = new FiltroVehiculo(EnumFiltroVehiculo.PorClave);
        //                fVehiculo.Usuario = entidad.Usuario;
        //                entidad.Clave = Consecutivo(fVehiculo);

        //                sentencia = new StringBuilder("INSERT INTO VEHICULOS");
        //                sentencia.AppendLine(" (USUARIO,CLAVE,TIPO,MARCA,MODELO,ANIO,VALOR,FORMAADQUISICION,PROPIETARIO,ESTATUS,OTROVEHICULO,ANIODECLARACION,TIPODECLARACION,TIPOMOVIMIENTO,FECHAMOVIMIENTO,COMENTARIOMOVIMIENTO,MONTOMOVIMIENTO )  ");
        //                sentencia.AppendLine(" VALUES (:USUARIO,:CLAVE,:TIPO,:MARCA,:MODELO,:ANIO,:VALOR,:FORMAADQUISICION,:PROPIETARIO,:ESTATUS,:OTROVEHICULO,:ANIODECLARACION,:TIPODECLARACION,:TIPOMOVIMIENTO,:FECHAMOVIMIENTO,:COMENTARIOMOVIMIENTO,:MONTOMOVIMIENTO )  ");
        //                comando = Conexion.ObtenerComando(conexion, sentencia.ToString(), transaccion);
        //                CrearParametros(ref comando, entidad);
        //                comando.ExecuteNonQuery();
        //            }
        //            else
        //            {
        //                sentencia = new StringBuilder("UPDATE VEHICULOS SET ");
        //                sentencia.AppendLine("TIPO = :TIPO, MARCA = :MARCA, MODELO = :MODELO, ANIO = :ANIO, VALOR = :VALOR, FORMAADQUISICION = :FORMAADQUISICION, PROPIETARIO = :PROPIETARIO, ESTATUS = :ESTATUS, OTROVEHICULO = :OTROVEHICULO, ANIODECLARACION = :ANIODECLARACION, TIPODECLARACION = :TIPODECLARACION, TIPOMOVIMIENTO = :TIPOMOVIMIENTO, FECHAMOVIMIENTO = :FECHAMOVIMIENTO, COMENTARIOMOVIMIENTO = :COMENTARIOMOVIMIENTO, MONTOMOVIMIENTO = :MONTOMOVIMIENTO ");
        //                sentencia.AppendLine(" WHERE USUARIO = :USUARIO AND CLAVE = :CLAVE ");
        //                comando = Conexion.ObtenerComando(conexion, sentencia.ToString(), transaccion);
        //                CrearParametros(ref comando, entidad);
        //                comando.ExecuteNonQuery();
        //            }

        //            if (entidad.InsertarEstatus == "SI")
        //            {
        //                sentencia = new StringBuilder(@" SELECT USUARIO FROM ESTATUSCAPTURAS WHERE USUARIO = :USUARIO AND APARTADO = :APARTADO AND ESTATUS= :ESTATUS AND TIPO = :TIPODEC AND ANIO = :ANIO ");
        //                comando = Conexion.ObtenerComando(conexion, sentencia.ToString(), transaccion);
        //                comando.Parameters.Add(new SqlParameter("USUARIO", entidad.Usuario));
        //                comando.Parameters.Add(new SqlParameter("APARTADO", entidad.Apartado));
        //                comando.Parameters.Add(new SqlParameter("ESTATUS", "E"));
        //                comando.Parameters.Add(new SqlParameter("TIPODEC", entidad.TipoDeclaracion));
        //                comando.Parameters.Add(new SqlParameter("ANIO", entidad.AnioDeclaracion));

        //                lector = comando.ExecuteReader();
        //                string _usuario = null;
        //                while (lector.Read())
        //                {
        //                    _usuario = !(lector["USUARIO"] is DBNull) ? lector["USUARIO"].ToString() : string.Empty;
        //                }
        //                lector.Close();
        //                if (_usuario == null)
        //                {
        //                    EstatusCaptura entidadEstatus = new EstatusCaptura();
        //                    entidadEstatus.Usuario = entidad.Usuario;
        //                    entidadEstatus.Estatus = "E";
        //                    entidadEstatus.Apartado = entidad.Apartado;
        //                    entidadEstatus.Tipo = entidad.TipoDeclaracion;
        //                    entidadEstatus.Anio = entidad.AnioDeclaracion;
        //                    AccesoDatos.ServiciosEstatusCaptura estatusCaptura = new AccesoDatos.ServiciosEstatusCaptura();
        //                    estatusCaptura.Insertar(entidadEstatus, conexion, transaccion);
        //                }
        //            }
        //            transaccion.Commit();
        //        }
        //        else
        //        {
        //            sentencia = new StringBuilder("UPDATE VEHICULOS SET ");
        //            sentencia.AppendLine("TIPO = :TIPO, MARCA = :MARCA, MODELO = :MODELO, ANIO = :ANIO, VALOR = :VALOR, FORMAADQUISICION = :FORMAADQUISICION, PROPIETARIO = :PROPIETARIO, ESTATUS = :ESTATUS, OTROVEHICULO = :OTROVEHICULO, ANIODECLARACION = :ANIODECLARACION, TIPODECLARACION = :TIPODECLARACION, TIPOMOVIMIENTO = :TIPOMOVIMIENTO, FECHAMOVIMIENTO = :FECHAMOVIMIENTO, COMENTARIOMOVIMIENTO = :COMENTARIOMOVIMIENTO, MONTOMOVIMIENTO = :MONTOMOVIMIENTO ");
        //            sentencia.AppendLine(" WHERE USUARIO = :USUARIO AND CLAVE = :CLAVE ");
        //            comando = Conexion.ObtenerComando(conexion, sentencia.ToString(), transaccion);
        //            CrearParametros(ref comando, entidad);
        //            comando.ExecuteNonQuery();

        //            if (entidad.InsertarEstatus == "SI")
        //            {
        //                sentencia = new StringBuilder(@" SELECT USUARIO FROM ESTATUSCAPTURAS WHERE USUARIO = :USUARIO AND APARTADO = :APARTADO AND ESTATUS= :ESTATUS AND TIPO = :TIPODEC AND ANIO = :ANIO ");
        //                comando = Conexion.ObtenerComando(conexion, sentencia.ToString(), transaccion);
        //                comando.Parameters.Add(new SqlParameter("USUARIO", entidad.Usuario));
        //                comando.Parameters.Add(new SqlParameter("APARTADO", entidad.Apartado));
        //                comando.Parameters.Add(new SqlParameter("ESTATUS", "E"));
        //                comando.Parameters.Add(new SqlParameter("TIPODEC", entidad.TipoDeclaracion));
        //                comando.Parameters.Add(new SqlParameter("ANIO", entidad.AnioDeclaracion));

        //                SqlDataReader lector = comando.ExecuteReader();
        //                string _usuario = null;
        //                while (lector.Read())
        //                {
        //                    _usuario = !(lector["USUARIO"] is DBNull) ? lector["USUARIO"].ToString() : string.Empty;
        //                }
        //                lector.Close();
        //                if (_usuario == null)
        //                {
        //                    EstatusCaptura entidadEstatus = new EstatusCaptura();
        //                    entidadEstatus.Usuario = entidad.Usuario;
        //                    entidadEstatus.Estatus = "E";
        //                    entidadEstatus.Apartado = entidad.Apartado;
        //                    entidadEstatus.Tipo = entidad.TipoDeclaracion;
        //                    entidadEstatus.Anio = entidad.AnioDeclaracion;
        //                    AccesoDatos.ServiciosEstatusCaptura estatusCaptura = new AccesoDatos.ServiciosEstatusCaptura();
        //                    estatusCaptura.Insertar(entidadEstatus, conexion, transaccion);
        //                }
        //            }
        //            transaccion.Commit();
        //        }

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        transaccion.Rollback();
        //        throw new Exception(ex.Message);
        //    }
        //    finally
        //    {
        //        conexion.Close();
        //        SqlConnection.ClearPool(conexion);
        //    }
        //}

        //public bool Eliminar(Usuario entidad)
        //{
        //    SqlConnection conexion = Conexion.ObtenerConexionDeclaracion();
        //    SqlTransaction transaccion = conexion.BeginTransaction();
        //    try
        //    {
        //        StringBuilder sentencia = new StringBuilder();
        //        if ((entidad.TipoDeclaracion == "M" || entidad.TipoDeclaracion == "C") && entidad.AnioDeclaracionAnterior < Convert.ToInt32(entidad.AnioDeclaracion))
        //        {
        //            sentencia = new StringBuilder("UPDATE VEHICULOS SET ");
        //            sentencia.AppendLine(" ESTATUS = :ESTATUS, TIPOMOVIMIENTO = :TIPOMOVIMIENTO, MONTOMOVIMIENTO = :MONTOMOVIMIENTO, FECHAMOVIMIENTO = :FECHAMOVIMIENTO, COMENTARIOMOVIMIENTO = :COMENTARIOMOVIMIENTO ");
        //            sentencia.AppendLine(" WHERE USUARIO = :USUARIO AND CLAVE = :CLAVE ");
        //            SqlCommand comando = Conexion.ObtenerComando(conexion, sentencia.ToString(), transaccion);
        //            comando.Parameters.Add(new SqlParameter("ESTATUS", "NO"));
        //            comando.Parameters.Add(new SqlParameter("USUARIO", entidad.Usuario));
        //            comando.Parameters.Add(new SqlParameter("CLAVE", entidad.Clave));
        //            comando.Parameters.Add(new SqlParameter("TIPOMOVIMIENTO", entidad.TipoMovimiento));
        //            comando.Parameters.Add(new SqlParameter("MONTOMOVIMIENTO", entidad.MontoMovimiento));
        //            comando.Parameters.Add(new SqlParameter("FECHAMOVIMIENTO", entidad.FechaMovimiento));
        //            comando.Parameters.Add(new SqlParameter("COMENTARIOMOVIMIENTO", string.Concat(entidad.ComentarioMovimiento, "-", entidad.AnioDeclaracion)));
        //            comando.ExecuteNonQuery();

        //            if (entidad.InsertarEstatus == "SI")
        //            {
        //                sentencia = new StringBuilder(@" SELECT USUARIO FROM ESTATUSCAPTURAS WHERE USUARIO = :USUARIO AND APARTADO = :APARTADO AND ESTATUS= :ESTATUS AND TIPO = :TIPODEC AND ANIO = :ANIO ");
        //                comando = Conexion.ObtenerComando(conexion, sentencia.ToString(), transaccion);
        //                comando.Parameters.Add(new SqlParameter("USUARIO", entidad.Usuario));
        //                comando.Parameters.Add(new SqlParameter("APARTADO", entidad.Apartado));
        //                comando.Parameters.Add(new SqlParameter("ESTATUS", "E"));
        //                comando.Parameters.Add(new SqlParameter("TIPODEC", entidad.TipoDeclaracion));
        //                comando.Parameters.Add(new SqlParameter("ANIO", entidad.AnioDeclaracion));

        //                SqlDataReader lector = comando.ExecuteReader();
        //                string _usuario = null;
        //                while (lector.Read())
        //                {
        //                    _usuario = !(lector["USUARIO"] is DBNull) ? lector["USUARIO"].ToString() : string.Empty;
        //                }
        //                lector.Close();
        //                if (_usuario == null)
        //                {
        //                    EstatusCaptura entidadEstatus = new EstatusCaptura();
        //                    entidadEstatus.Usuario = entidad.Usuario;
        //                    entidadEstatus.Estatus = "E";
        //                    entidadEstatus.Apartado = entidad.Apartado;
        //                    entidadEstatus.Tipo = entidad.TipoDeclaracion;
        //                    entidadEstatus.Anio = entidad.AnioDeclaracion;
        //                    AccesoDatos.ServiciosEstatusCaptura estatusCaptura = new AccesoDatos.ServiciosEstatusCaptura();
        //                    estatusCaptura.Insertar(entidadEstatus, conexion, transaccion);
        //                }
        //            }

        //            transaccion.Commit();

        //            return true;
        //        }
        //        else
        //        {
        //            sentencia = new StringBuilder("DELETE FROM VEHICULOS ");
        //            sentencia.AppendLine(" WHERE USUARIO = :USUARIO AND CLAVE = :CLAVE ");
        //            SqlCommand comando = Conexion.ObtenerComando(conexion, sentencia.ToString(), transaccion);
        //            comando.Parameters.Add(new SqlParameter("USUARIO", entidad.Usuario));
        //            comando.Parameters.Add(new SqlParameter("CLAVE", entidad.Clave));
        //            comando.ExecuteNonQuery();
        //            transaccion.Commit();
        //        }

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        transaccion.Rollback();
        //        throw new Exception(ex.Message);
        //    }
        //    finally
        //    {
        //        conexion.Close();
        //        SqlConnection.ClearPool(conexion);
        //    }
        //}


        #endregion


    }
}
