$(function(){
    consultarOpinion();
});
function consultarOpinion() {
	var url = $("#hdnUrlGetlistOpinion").val();
	$.ajax({
		url: url +"/2",
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
function pintarOpinion(opinion){
	var html ="";
}

  