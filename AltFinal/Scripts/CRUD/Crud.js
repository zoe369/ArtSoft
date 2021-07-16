function fnInsert() {
	var FirstName = $("#Fname").val();
	var LastName = $("#Lname").val();
	var Mobno = $("#Mobno").val();
	var Adress = $("#Adress").val();


	$.ajax({
		type: "POST",
		url: "/Home/InsertFields/",
		datatype: "json",
		contentType: "application/json: charset=utf-8",
		data: JSON.stringify({ FirstName: FirstName, LastName: LastName, Mobno: Mobno, Adress: Adress }),
		success: function (json1) {
			ViewData1();
		},
		failure: function (errMsg) {
			alert(errMsg);
		}
	});
}

$(document).ready(function () {
	ViewData1();
	$("#update").hide();
	function ViewData1() {

		$.ajax({
			type: "POST",
			url: '/Home/ViewData1/',
			dataType: "json",
			contentType: "application/json: charset=utf-8",
			data: JSON.stringify({}),
			succes: function (json1) {
				console.log(json1);
				var tableload = json1.html;
				var dataset = eval("[" + tableload + "]");
				$('#ViewValues').DataTable({
					ordering: false,
					data: dataset,
					columns: [
						{ title: "ID" },
						{ title: "First Name" },
						{ title: "Last Name" },
						{ title: "Mobile Number" },
						{ title: "Adress" },
						{ title: "Action"},
					]
				});

			},
			error: function (err) {
				console.log(err);
            },
			failure: function (errMsg) {
				alert(errMsg);
			}
		});
	}
});


function fnDelete(id) {
	$.ajax({
		type: "POST",
		url: '/Home/DeleteData/',
		datatype: "json",
		contentType: "application/json: charset=utf-8",
		data: JSON.stringify({ id: id }),
		success: function (json) {
			window.location.replace("/Home/Index/");
		},
		failure: function (errMsg) {
			alert(errMsg);
		}
	});
}

function fnEdit(id) {
	$.ajax({
		type: "POST",
		url: '/Home/EditData/',
		datatype: "json",
		contentType: "application/json: charset=utf-8",
		data: JSON.stringify({ id: id }),
		success: function (json) {
			var arrval = json.htmlValues;

			$("#Fname").val(arrval[0]);
			$("#Lname").val(arrval[1]);
			$("#Mobno").val(arrval[2]);
			$("#Adress").val(arrval[3]);
			$("#id").val(arrval[4]);

			$("#create").hide();
			$("#update").show();

		},
		failure: function (errMsg) {
			alert(errMsg);
		}
	});
}

function fnUpdate() {
	var FirstName = $("#Fname").val();
	var LastName = $("#Lname").val();
	var Mobno = $("#Mobno").val();
	var Adress = $("#Adress").val();
	var id = $("#id").val();


	$.ajax({
		type: "POST",
		url: "/Home/UpdateFields/",
		datatype: "json",
		contentType: "application/json: charset=utf-8",
		data: JSON.stringify({ FirstName: FirstName, LastName: LastName, Mobno: Mobno, Adress: Adress, id: id }),
		success: function (json1) {
			ViewData1();

			window.location.replace("/Home/Index/");
		},
		failure: function (errMsg) {
			alert(errMsg);
		}
	});
}