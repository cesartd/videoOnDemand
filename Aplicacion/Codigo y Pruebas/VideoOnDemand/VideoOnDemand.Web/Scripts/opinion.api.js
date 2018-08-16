$(function(){
    consultarOpinion();
});

function consultarOpinion() {
    var url = $("#hdnUrlGetlistOpinion").val();
    var id = document.getElementById('serieId').textContent;
    console.log(id);

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
    

    var html = "<li class='list-group-item'>";
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
        html += "<blockquote class='blockquote'>";
        html += "<p class='mb-0'>"+opinion.Descripcion +"</p>";
        html += "</blockquote>";
        html += "</div>";
        html += "</div>";
        html += "</li>";
    $("#listaOpiniones").append(html);
}

  