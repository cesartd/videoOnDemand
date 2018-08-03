using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using VideoOnDemand.Entities;
using VideoOnDemand.Repositories;
using VideoOnDemand.Web.Helpers;
using VideoOnDemand.Web.Models;

namespace VideoOnDemand.Web.Controllers
{
    public class PersonaController : BaseController
    {
        // GET: Persona
        public ActionResult Index(string Search)
        {
            PersonaRepository repository = new PersonaRepository(context); //Se agrega esto
            Persona persona = new Persona();
            persona.Nombre = Search;

            ICollection<Persona> list = null;

            if (!String.IsNullOrEmpty(Search))
            {
                list = repository.QueryByExample(persona);
                
            }
            else {

                list = repository.GetAll().ToList();
            }
            var models = MapHelper.Map<IEnumerable<PersonaViewModel>>(list); //Se agrega esto
            return View(models); //Retorna a models
          
        }

        // GET: Persona/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Persona/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Persona/Create
        [HttpPost]
        public ActionResult Create(PersonaViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // TODO: Add insert logic here
                    PersonaRepository repository = new PersonaRepository(context);

                    #region Validaciones
                    // Validar nombre único
                    var topicQry = new Persona { Nombre = model.Nombre };

                    // Consulto los temas con el nombre
                    bool existeTopic = repository.QueryByExample(topicQry).Count > 0;

                    if (existeTopic)
                    {
                        ModelState.AddModelError("Name", "El nombre del tema ya existe.");
                        return View(model);
                    }
                    #endregion

                    // Variable auxiliar que sirve de mapeo de TopicViewModel a Topic
                    Persona persona = MapHelper.Map<Persona>(model);

                    // Se inserta el valor.
                    repository.Insert(persona);

                    // Guardar y registrar cambios.
                    context.SaveChanges();

                    return RedirectToAction("Index");
                }

                return View(model);

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }

        // GET: Persona/Edit/5
        public ActionResult Edit(int id)
        {
            PersonaRepository repository = new PersonaRepository(context);

            var persona = repository.Query(t => t.Id == id).First();

            var model = MapHelper.Map<PersonaViewModel>(persona);

            return View(model);
            
        }

        // POST: Persona/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, PersonaViewModel model)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    PersonaRepository repository = new PersonaRepository(context);

                    #region Validaciones
                    // Consulto los temas con el nombre
                    bool existePersona = repository.Query(x => x.Nombre == model.Nombre && x.Id != model.Id)
                        .Count > 0;

                    if (existePersona)
                    {
                        ModelState.AddModelError("Name", "El nombre del tema ya existe.");
                        return View(model);
                    }
                    #endregion

                    // Variable auxiliar que sirve de mapeo de TopicViewModel a Topic
                    Persona persona = MapHelper.Map<Persona>(model);

                    // Se modifica el valor.
                    repository.Update(persona);

                    // Guardar y registrar cambios.
                    context.SaveChanges();

                    return RedirectToAction("Index");
                }

                return View(model);
            }
            catch
            {
                return View(model);
            }
        }

        // GET: Persona/Delete/5
        public ActionResult Delete(int id)
        {
            PersonaRepository repository = new PersonaRepository(context);
            var persona = repository.Query(t => t.Id == id).First();
            var model = MapHelper.Map<PersonaViewModel>(persona);

            return View(model);
        }

        // POST: Persona/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, PersonaViewModel model)//Cambiar
        {
            try
            {
                // TODO: Add delete logic here
                PersonaRepository repository = new PersonaRepository(context);

                Persona persona = MapHelper.Map<Persona>(model);

                repository.Delete(persona);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Buscador(String Search )
        {
            PersonaRepository repository = new PersonaRepository(context); 
            var list = repository.GetAll(); 

            var busqueda = from s in list  select s;
            if (!String.IsNullOrEmpty(Search))
            {
                busqueda = busqueda.Where(j => j.Nombre.Contains(Search));
            }
            return View(busqueda);
        }

    }
}
