$(function(){
    consultarOpinion();
});

function consultarOpinion() {
    var url = $("#hdnUrlGetlistOpinion").val();
    var id = $("#serieId").val();

    $("#listaOpinones").empty();
	$.ajax({
		url: url +"/"+id,
		type: 'GET',
		dataType: 'JSON',
		data: null,
		success: function (data) {
			if (data.Success) {
				var opiniones = JSON.parse(data.Opiniones);
				$("#listaOpinones").empty();
				for (i = 0; i < opiniones.length; i++) {
					pintarOpinion(opiniones[i]);

				}
			}
                    
		}
	})

}

function pintarOpinion(opinion) {

    var date = opinion.FechaRegistro;
    date = date.replace(/-/g, '/');
    date = date.replace('T', ' ');

    var rate = opinion.Puntuacion;
    var usuarioId = $("#usuarioId").val();

    var html = "<li id='opinion-container-"+opinion.Id+"' class='list-group-item' style='background-color:black;'>";
        html +="<div class='row'>";
        html +="<div class='col-sm-1'>";
        html +="<img class='rounded-circle' src='/Content/Media/chip.jpg' style='width:50px; height:50px'/>";
        html +="</div>";
        html +="<div class='col-sm-11'>";
        html += "<label>"+opinion.Usuario.Nombre+"</label><label>-</label><label>"+date+"</label>";
        html += "&nbsp;&nbsp;<span class='fa fa-star checked'></span>";
        for (var i = 1; i <rate; i++) {
            html += "<span class='fa fa-star checked'></span>";
        }
        if (opinion.UsuarioId == usuarioId) {

            html += "<button type='button' class='btn btn-primary' onclick='eliminar(" + opinion.Id + ");'>Eliminar</button>";
            limpiar();
        }
        html += "<blockquote class='blockquote'>";
        html += "<p class='mb-0'>"+opinion.Descripcion +"</p>";
        html += "</blockquote>";
        html += "</div>";
        html += "</div>";
        html += "</li>";
    $("#listaOpiniones").append(html);
}
function crear() {
  
    var comentario = $("#tbxComentarios").val();
    var puntuacion = $("input:radio[name=rdPuntuacion]:checked").val();
    var mediaId = $("#serieId").val();
    var opinion = {
        Puntuacion: puntuacion, Descripcion: comentario, MediaId: mediaId
    };

    var url = $("#hdnUrlCreatelistOpinion").val();
    $.ajax({
        url: url,
        type: 'POST',
        dataType: 'JSON',
        contentType: "application/json",
        data: JSON.stringify(opinion),
        success: function (data) {
            if (data.Success) {
                limpiar();
                consultarOpinion();
            }

        }
    });

}
function eliminar(id) {
       var opinion = {
       Id: id
    };

    var url = $("#hdnUrlDeletelistOpinion").val();
    $.ajax({
        url: url,
        type: 'POST',
        dataType: 'JSON',
        contentType: "application/json",
        data: JSON.stringify(opinion),
        success: function (data) {
            if (data.Success) {
                $("#listaOpinones").empty();
                mostrarFormulario();
                $("#opinion-container-" + id).remove();
                consultarOpinion();
            }

        }
    });
}
function limpiar() {
    $("#contenidoComentarios").hide();
    $("#tbxComentarios").val("");
    //Falta desmarcar RadioButton
      
}
function mostrarFormulario() {
    $("#contenidoComentarios").show();

}
