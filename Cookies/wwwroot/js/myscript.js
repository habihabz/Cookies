﻿$(document).ready(function () {

    $("#rq_cre_by").select2();
    $("#rq_dep_id").select2();
    $("#rq_cre_for").select2();
    $("#priceType").select2();
    $('#saveMenu').click(function () {
      
        var reqRow = [];
        $("#multiselect_to option").each(function () {
            reqRow.push($(this).val());
        });

        var value = $('#SelectRole').val();
        if (value == 0) {
            alert("Please Choose Role");
        } else {
            var type = $('input[name=frmTypes]:checked').val();
            var data = new FormData();

            data.append('role', value);
            data.append('type', type);
            data.append('menus', reqRow);
            $.ajax({
                url: "/RoleMenu/saveMenuitems",
                type: "POST",
                contentType: false,
                processData: false,
                cache: false,
                data: data,
                success: function (response) {
                    alert(response);
                    $("#multiselect_to").empty();
                    $("#multiselect").empty();
                },
                error: function () {
                    $("#multiselect_to").empty();
                    $("#multiselect").empty();

                }
            });
        }
    });

    $("#SelectRole").change(function () {
        load_user_menus();
    });
    $("#multiselect_rightSelected").click(function () {
        if ($("#multiselect_to option[value='" + $("#multiselect").val()+"']").length ==0) {
            $("#multiselect_to").append($("<option />").val($("#multiselect option:selected").val()).text($("#multiselect option:selected").text()));
            $("#multiselect option[value='" + $("#multiselect option:selected").val()+"']").remove();
        }
    });
    $("#multiselect_rightAll").click(function () {
        if ($('#multiselect option').length != 0) {
            $("#multiselect_to").empty();
            $("#multiselect option").each(function () {

                if ($("#multiselect_to option[value='" + $(this).val() + "']").length == 0) {
                    $("#multiselect_to").append($("<option />").val($(this).val()).text($(this).text()));
                    $("#multiselect option[value='" + $(this).val() + "']").remove();
                }
            });
        }    
    });
    $("#multiselect_leftSelected").click(function () {
        if ($("#multiselect option[value='" + $("#multiselect_to").val() + "']").length == 0) {
           
            $("#multiselect").append($("<option />").val($("#multiselect_to option:selected").val()).text($("#multiselect_to option:selected").text()));
            $("#multiselect_to option[value='" + $("#multiselect_to option:selected").val() + "']").remove();
        }
    });
    $("#multiselect_leftAll").click(function () {
        if ($('#multiselect_to option').length != 0) {
            $("#multiselect").empty();
            $("#multiselect_to option").each(function () {

                if ($("#multiselect option[value='" + $(this).val() + "']").length == 0) {
                    $("#multiselect").append($("<option />").val($(this).val()).text($(this).text()));
                    $("#multiselect_to option[value='" + $(this).val() + "']").remove();
                }
            });
        }
    });
    $('.multiselect').multiselect();
    $('#forWhom').change(function() {
        if (!$(this).is(":checked")) {
            var returnVal = confirm("Are you sure?");
            $(this).attr("uncheck", returnVal);
        }
        else {
            alert();
        }
              
    });
    $('[data-toggle="tooltip"]').tooltip(); 
    if ($("#demo").length) {
        setInterval(myTimer, 1000);
    }
    if ($("#liveMonitoring").length) {
       setInterval(loadTimeForAllTr, 1000);
        //loadTimeForAllTr();
    }
    if ($("#mytable").length) {
        $('#mytable').DataTable({
            dom: 'lBfrtip',
            buttons: [
                'copyHtml5',
                'excelHtml5',
                'pdfHtml5'
            ]
         } );
    }
    if ($("#table2").length) {
        $('#table2').DataTable({
            dom: 'lBfrtip',
            buttons: [
                'copyHtml5',
                'excelHtml5',
                'pdfHtml5'
            ]
        });
    }
    if ($("#Products").length) {
        getProducts();
    }
    if ($("#Customers").length) {
        getCustomers();
    }
    
    $("#name").autocomplete({

        source: function (req, resp) {
            var data = new FormData();
            data.append('name', $("#name").val());
            $.ajax({
                url: "/CookiesRequest/UsersName",
                type: "POST",
                contentType: false,
                processData: false,
                cache: false,
                data: data,
                success: function (data) {
                    resp(data);
                },
                error: function () {
                }
            });
        },
        select: function (event, ui) {

            var TABKEY = 9;
            this.value = ui.item.value;

            if (event.keyCode == TABKEY) {
                event.preventDefault();
                $('#name').focus();
            }

            return false;
        },
        position: {
            offset: '1000 4' // Shift 20px to the left, 4px down.
        }
    });
   
    $('input[type=radio][name=frmTypes]').change(function () {

        load_user_menus();
    });
    $('#role').change(function () {
        load_user_menus()
    });
   

});

function openWorkflowDetl(id) {
        var data = new FormData();
        data.append('ID', id);
        $.ajax({
            url: "/Workflow/Details",
            type: "POST",
            contentType: false,
            processData: false,
            cache: false,
            data: data,
            success: function (response) {
                $("#modalContainer").html(response);
                $("#wd_workflow_id").val(id);
                $('#myModal').modal('show');

            },
            error: function () {
            }
        });
}

function saveWorkFlowDetails() {
    var workflowid = $("#wd_workflow_id").val();
    var role_id = $("#wd_role_id").val();
    var priority = $("#wd_priority").val();

    var data = new FormData();
    data.append('wd_workflow_id', workflowid);
    data.append('wd_role_id', role_id);
    data.append('wd_priority', priority);
    $.ajax({
        url: "/WorkflowDetail/Create",
        type: "POST",
        contentType: false,
        processData: false,
        cache: false,
        data: data,
        success: function (response) {
            $("#modalContainer").html(response);
            $('#myModal').modal('show');
            $("#wd_priority").val("");

        },
        error: function () {
        }
    });
}


function CookiesRequestReport() {
    var data = new FormData();
    data.append("rq_dep_id", $("#rq_dep_id").val());
    data.append("reportrange", $("#reportrange").val());
    data.append("rq_cre_by", $("#rq_cre_by").val());
    data.append("rq_cre_date", $("#rq_cre_date").val());
    $.ajax({
        url: "/CookiesRequest/CustomReport",
        type: "POST",
        contentType: false,
        processData: false,
        cache: false,
        data: data,
        success: function (response) {
            $("#container").html(response);
            $('#mytable').DataTable({
                dom: 'lBfrtip',
                buttons: [
                    'copyHtml5',
                    'excelHtml5',
                    'pdfHtml5'
                ]
            });
        },
        error: function () {
        }
    });
}

function workflowHistory(rowid,doc_id,workflow,status) {
    var data = new FormData();
    data.append("rowid", rowid);
    data.append("doc_id", doc_id);
    data.append("workflow", workflow);
    
    $.ajax({
        url: "/WorkflowTracker/History",
        type: "POST",
        contentType: false,
        processData: false,
        cache: false,
        data: data,
        success: function (response) {
            workflowDetailStatus(workflow, status);
            $("#modalContainer").html(response);
            $('#historyModal').modal('show');

        },
        error: function () {
        }
    });
}

function workflowDetailStatus( workflow,status) {
    var data = new FormData();
    data.append("workflow", workflow);
    data.append("status", status); 
    $.ajax({
        url: "/WorkflowDetail/Status",
        type: "POST",
        contentType: false,
        processData: false,
        cache: false,
        data: data,
        success: function (response) {
            $("#cont").html(response);
          
        },
        error: function () {
        }
    });
}

function CookiesConsolidatedReport() {
    var data = new FormData();
    data.append("rq_dep_id", $("#rq_dep_id").val());
    data.append("rq_cre_for", $("#rq_cre_for").val());
    data.append("reportrange", $("#reportrange").val());
    $.ajax({
        url: "/CookiesRequest/ConsolidatedReports",
        type: "POST",
        contentType: false,
        processData: false,
        cache: false,
        data: data,
        success: function (response) {
            $("#container").html(response);
            $('#mytable').DataTable({
                dom: 'lBfrtip',
                buttons: [
                    'copyHtml5',
                    'excelHtml5',
                    'pdfHtml5'
                ]
            });
        },
        error: function () {
        }
    });
}
$(function () {

    var start = moment().subtract(29, 'days');
    var end = moment();

    function cb(start, end) {
        $('#reportrange span').html(start.format('MMMM D, YYYY') + ' - ' + end.format('MMMM D, YYYY'));
    }

    $('#reportrange').daterangepicker({
        startDate: start,
        endDate: end,
        ranges: {
            'Today': [moment(), moment()],
            'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
            'Last 7 Days': [moment().subtract(6, 'days'), moment()],
            'Last 30 Days': [moment().subtract(29, 'days'), moment()],
            'This Month': [moment().startOf('month'), moment().endOf('month')],
            'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')]
        }
    }, cb);

    cb(start, end);

});



function myTimer() {
 var date1 = new Date();
  //  var date2 = Date.parse("2020/05/29 21:59:00");
    var date2 = Date.parse($("#lbl_start").text());
var delta = Math.abs(date1 - date2) / 1000;

// calculate (and subtract) whole days
var days = Math.floor(delta / 86400);
delta -= days * 86400;

// calculate (and subtract) whole hours
var hours = Math.floor(delta / 3600) % 24;
delta -= hours * 3600;

// calculate (and subtract) whole minutes
var minutes = Math.floor(delta / 60) % 60;
delta -= minutes * 60;

// what's left is seconds
var seconds = delta % 60;
    document.getElementById("demo").innerHTML = days + ": " + hours + ":" + minutes + ":" + Math.round(seconds);
}


function loadTimeForAllTr() {
    if ($("#mytable").length) {
        
        $("tr.a").each(function (i, tr) {
            var value = $(this).find("input.b").val();
           
            var date1 = new Date();
            //  var date2 = Date.parse("2020/05/29 21:59:00");
            var date2 = Date.parse($(this).find("#starttime" + value).val() );
            var delta = Math.abs(date1 - date2) / 1000;

            // calculate (and subtract) whole days
            var days = Math.floor(delta / 86400);
            delta -= days * 86400;

            // calculate (and subtract) whole hours
            var hours = Math.floor(delta / 3600) % 24;
            delta -= hours * 3600;

            // calculate (and subtract) whole minutes
            var minutes = Math.floor(delta / 60) % 60;
            delta -= minutes * 60;

            // what's left is seconds
            var seconds = delta % 60;
            $(this).find("#Duration" + value).text(n(days) + ": " + n(hours) + ":" + n(minutes) + ":" + n(Math.round(seconds)));
        });
    }

}

function n(n) {
    return n > 9 ? "" + n : "0" + n;
}

function holdHistory(rowid, doc_id,from) {
    var data = new FormData();
    data.append("rowid", rowid);
    data.append("doc_id", doc_id);
    data.append("from", from);

    $.ajax({
        url: "/Hold/History",
        type: "POST",
        contentType: false,
        processData: false,
        cache: false,
        data: data,
        success: function (response) {
            $("#modalContainer").html(response);
            $('#historyModal').modal('show');
        },
        error: function () {
        }
    });
}
function replayForHold(id) {
    var data = new FormData();
    data.append("id", id);
    data.append("replay", $("#replay" + id).val().replace(/[\n\r]/g, ''));
    $.ajax({
        url: "/Hold/Replay",
        type: "POST",
        contentType: false,
        processData: false,
        cache: false,
        data: data,
        success: function (response) {
        },
        error: function () {
        }
    });
}
function hold(id) {
    var data = prompt("Reason For Blocking?? ", "");
    if (data != null) {
        $("#reason" + id).val(data);
        $("#hold" + id).submit();
    }
   
}
function unhold(id) {
    var data = prompt("Reason For Blocking?? ", "");
    if (data != null) {
        $("#reason" + id).val(data);
        $("#unhold" + id).submit();
    }
   
}

function load_user_menus() {
    var $option = $('#SelectRole').find('option:selected');
    var role = $option.val();

    var type = $('input[name=frmTypes]:checked').val();
    var data = new FormData();
    data.append('role', role);
    data.append('type', type);
    $.ajax({
        url: "/RoleMenu/showRoleMenus",
        type: "POST",
        contentType: false,
        processData: false,
        cache: false,
        data: data,
        success: function (response) {
            var data = JSON.parse(response);
            var i;
            if ($.isEmptyObject(response)) {
                $("#multiselect_to").empty();
                $("#multiselect").empty();
            } else {
              
                $('#saveMenu').prop('disabled', false);
                $("#multiselect_to").empty();
                $("#multiselect").empty();
                for (i = 0; i < data.userMenu.length; ++i) {
                    $("#multiselect_to").append($("<option />").val(data.userMenu[i].m_id).text(data.userMenu[i].m_description));
                   
                }
                for (i = 0; i < data.allMenu.length; ++i) {
                    $("#multiselect").append($("<option />").val(data.allMenu[i].m_id).text(data.allMenu[i].m_description));

                }
            }
        },
        error: function () {
            
            $("#multiselect_to").empty();
            $("#multiselect").empty();
        }
    });
}

function viewInsights(id,doc_id,flag) {
    var data = new FormData();
    data.append('id', id);
    data.append('doc_id', doc_id);
    $.ajax({
        url: "/Insights/index",
        type: "POST",
        contentType: false,
        processData: false,
        cache: false,
        data: data,
        success: function (response) {
            $("#insightsContainer").html(response);
            if (flag == 0) {
                $("#filter").empty()
            }
            $("#id").val(id);
            $("#doc_id").val(doc_id);
            $('#insightsModal').modal('show');
        },
        error: function () {
        }
    });
}

function saveInsight() {
    var id = $("#id").val();
    var doc_id = $("#doc_id").val();
    var in_remarks = $("#remarks").val();

    var data = new FormData();
    data.append('id', id);
    data.append('doc_id', doc_id);
    data.append('remarks', in_remarks);
    $.ajax({
        url: "/Insights/Create",
        type: "POST",
        contentType: false,
        processData: false,
        cache: false,
        data: data,
        success: function (response) {
            $("#insightsContainer").html(response);
            $("#id").val(id);
            $("#doc_id").val(doc_id);
            $('#insightsModal').modal('show');
            $("#remarks").val("");

        },
        error: function () {
        }
    });
}

function Reject(id) {
    var data = prompt("Reason For Rejection?? ", "");
    if (data != null) {
        $("#reason" + id).val(data);
        $("#reject" + id).submit();
    }
}

function consolidatedByType() {
    var data = new FormData();
    data.append("type", $("#type").val());
    if ($("#type").val() != "") {
        data.append("reportrange", $("#reportrange").val());
        $.ajax({
            url: "/CookiesRequest/consolidateReportByType",
            type: "POST",
            contentType: false,
            processData: false,
            cache: false,
            data: data,
            success: function (response) {
                $("#container").html(response);

                if ($("#type").val() == "Department") {
                    $('td[name^="emp"]').remove();
                    $('th[name^="emp"]').remove();
                }
                else {
                    $('td[name^="dep"]').remove();
                    $('th[name^="dep"]').remove();
                }
                $('#mytable').DataTable({
                    dom: 'lBfrtip',
                    buttons: [
                        'copyHtml5',
                        'excelHtml5',
                        'pdfHtml5'
                    ]
                });
            },
            error: function () {
            }
        });
    } else {
        alert("Please choose Report type");
    }
    
}
function loadDataTableById(id) {
    $('#' + id).DataTable({
        "pageLength": 25,
        order: [],
        dom: 'lBfrtip',
        buttons: [
            { extend: 'excel', title: '' },
            'copyHtml5',
            'pdfHtml5'
        ]
    });
}

function getProducts() {
    var data = new FormData();
    $.ajax({
        url: "/Product/getProducts",
        type: "POST",
        contentType: false,
        processData: false,
        cache: false,
        data: data,
        success: function (response) {
            $("#container").html(response);
            loadDataTableById("mytable");
        },
        error: function () {
        }
    });
}

function OpenCreateProductModal() {
    $("#p_id").val("0");
    $("#productModalHeader").html("Create Product");
    $("#productModal").modal("show");
}

function createOrEditProduct() {
    var p_name = $("#p_name").val();
    var p_id = $("#p_id").val();

    if (p_name != "") {

        var data = new FormData();
        data.append("p_id", p_id);
        data.append("p_name", p_name);

        $.ajax({
            url: "/Product/createOrEditProduct",
            type: "POST",
            contentType: false,
            processData: false,
            cache: false,
            data: data,
            success: function (response) {
                if (response.message == "Success") {
                    getProducts();
                    $("#p_name").val("");
                  
                }
                else {
                    alert(response.message);
                }
            },
            error: function () {
            }
        });

    }
}

function editProduct(id) {
    
        var data = new FormData();
        data.append("id", id);

        $.ajax({
            url: "/Product/getProduct",
            type: "POST",
            contentType: false,
            processData: false,
            cache: false,
            data: data,
            success: function (response) {
                $("#productModalHeader").html("Edit Product");
                $("#productModal").modal("show");
                $("#p_id").val(id);
                $("#p_name").val(response.p_name);
         
       
            },
            error: function () {
            }
        });

}
function removeProduct(id) {

    var data = new FormData();
    data.append("id", id);

    $.ajax({
        url: "/Product/removeProduct",
        type: "POST",
        contentType: false,
        processData: false,
        cache: false,
        data: data,
        success: function (response) {
            if (response.message == "Success") {
                var datatable = $("#mytable").DataTable();
                datatable.row($("#tr" + id)).remove().draw();

            }
            else {
                alert(response.message);
            }
     
        },
        error: function () {
        }
    });
}


///////////

function getCustomers() {
    var data = new FormData();
    $.ajax({
        url: "/Customer/getCustomers",
        type: "POST",
        contentType: false,
        processData: false,
        cache: false,
        data: data,
        success: function (response) {
            $("#container").html(response);
            loadDataTableById("mytable");
        },
        error: function () {
        }
    });
}

function OpenCreateCustomerModal() {
    $("#c_id").val("0");
    $("#customerModalHeader").html("Create Customer");
    $("#customerModal").modal("show");
}

function createOrEditCustomer() {
    var c_name = $("#c_name").val();
    var c_id = $("#c_id").val();
    var c_price_type = $("#c_price_type").val();

    if (c_name != "") {

        var data = new FormData();
        data.append("c_id", c_id);
        data.append("c_name", c_name);
        data.append("c_price_type", c_price_type);

        $.ajax({
            url: "/Customer/createOrEditCustomer",
            type: "POST",
            contentType: false,
            processData: false,
            cache: false,
            data: data,
            success: function (response) {
                if (response.message == "Success") {
                    getCustomers();
                    $("#c_name").val("");
                    $("#c_price_type").select2().val("").trigger("change");
                }
                else {
                    alert(response.message);
                }
            },
            error: function () {
            }
        });

    }
}

function editCustomer(id) {

    var data = new FormData();
    data.append("id", id);

    $.ajax({
        url: "/Customer/getCustomer",
        type: "POST",
        contentType: false,
        processData: false,
        cache: false,
        data: data,
        success: function (response) {
            $("#customerModalHeader").html("Edit Customer");
            $("#customerModal").modal("show");
            $("#c_id").val(id);
            $("#c_name").val(response.c_name);
            $("#c_price_type").select2().val(response.c_price_type).trigger("change");

        },
        error: function () {
        }
    });

}
function removeCustomer(id) {

    var data = new FormData();
    data.append("id", id);

    $.ajax({
        url: "/Customer/removeCustomer",
        type: "POST",
        contentType: false,
        processData: false,
        cache: false,
        data: data,
        success: function (response) {
            if (response.message == "Success") {

                var datatable = $("#mytable").DataTable();
                datatable.row($("#tr" + id)).remove().draw();
            }
            else {
                alert(response.message);
            }

        },
        error: function () {
        }
    });
}

function openPriceModal(id) {

    $("#pr_prod_id").val(id);
    $("#priceModal").modal("show");
    GetActivePricesForAProduct(id);

}

function AddProductPrice() {

    var pr_prod_id = $("#pr_prod_id").val();
    var pr_price_type = $("#pr_price_type").val();
    var pr_price = $("#pr_price").val();
    var pr_start_date = $("#pr_start_date").val();


    var data = new FormData();
    data.append("pr_prod_id", pr_prod_id);
    data.append("pr_price_type", pr_price_type);
    data.append("pr_price", pr_price);
    data.append("pr_start_date", pr_start_date);
    $.ajax({
        url: "/Product/AddProductPrice",
        type: "POST",
        contentType: false,
        processData: false,
        cache: false,
        data: data,
        success: function (response) {
            if (response.message == "Success") {
                GetActivePricesForAProduct(pr_prod_id);
            }
            else {
                alert(response.message);
            }

        },
        error: function () {
        }
    });
}
function GetActivePricesForAProduct(prod_id) {

    var data = new FormData();
    data.append("prod_id", prod_id);
    $.ajax({
        url: "/Product/GetActivePricesForAProduct",
        type: "POST",
        contentType: false,
        processData: false,
        cache: false,
        data: data,
        success: function (response) {
            $("#priceContainer").html(response);
            
        },
        error: function () {
        }
    });

}