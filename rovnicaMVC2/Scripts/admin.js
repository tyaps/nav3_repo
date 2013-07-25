
var itemDetailsChanges = {};

$(document).ready(function() {
    $('.photoimg').live('change', function() {

        var form = $(this).closest(".imageform")
        form.find("#preview").html('Загрузка пошла');
        //$("#preview").html('<img src="loader.gif" alt="Uploading...."/>');
        form.ajaxForm(
        {

        success: function(data) {
            form.find(".resultImg").attr("src", imgPath + data);
        }
    }).submit();
    });
});





function showOrderDetails(id) {
    $("#d" + id).toggle();
    $("#c" + id).toggle();
}



function createNewItem() {
    $.ajax({
        url: createNewItemUrl,
        cache: false,
        data: { name: $("#tbItemName").val(), price: $("#tbItemPrice").val(), categoryId: $("#ddlCategory").val() },

        success: function(data) {
            
            $("#itemDetails").remove();
            $("body").append(data);
        },
        error: function() {

        }

    });
}

function showItemDetails(id) {
    $.ajax({
        url: itemDetailsUrl,
        cache: false,
        data: { id: id },

        success: function(data) {

            $("#itemDetails").remove();
            $("body").append(data);
            resetItemDetailsChanges();
        },
        error: function() {

        }

    });
}

function showWarehouseTotalAmount(id) {
    $.ajax({
        url: warehouseTotalAmountUrl,
        cache: false,
        data: { itemId: id },

        success: function(data) {

            $("#itemDetails #warehouse").empty();
            $("#itemDetails #warehouse").append(data);
            
        },
        error: function() {

        }

    });
}

function saveWarehouseAmounts() {

    $("#itemDetails #warehouse input:text").each(function() {

        var waId = $(this).attr("waId");
        var amount = $(this).val();
        var status = $(this).next().attr("checked") ? 1 : 0;

        $.ajax({
            url: warehouseTotalAmountUpdateUrl,
            cache: false,
            data: { warehousemountId: waId, amount: amount, status: status },

            success: function(data) {
            //todo: надо что-то вывести
            }
            

        });

    });

}

function addMoreImageTemplate(itemId) {

    var imgNumber = $("#itemImages tr:eq(0) > td").length + 1;
    
    $.ajax({
    url: addMoreImageTemplateUrl,
        cache: false,
        data: { itemId: itemId, imgNumber: imgNumber },

        success: function(data) {

        $("#itemImages tr:eq(0)").append("<td>" + data + "</td>");
        },
        error: function() {

        }

    });
}
function resetItemDetailsChanges() {
    itemDetailsChanges = {};
    itemDetailsChanges.removedColors = new Array();
    itemDetailsChanges.addedColors = new Array();
    itemDetailsChanges.removedSizes = new Array();
    itemDetailsChanges.addedSizes = new Array();
}

function addColor() {
    $("#lbColors").append("<option value='0'>" + $("#tbAddColor").val() + "</option>");
}
function removeColor() {
    $("#lbColors option:selected").each(function() {

        if($(this).attr("id")!=null)
            itemDetailsChanges.removedColors.push($(this).val());
            
        $(this).remove();
    });
}
function addSize() {
    $("#lbSizes").append("<option value='0'>" + $("#tbAddSize").val() + "</option>");
}
function removeSize() {
    $("#lbSizes option:selected").each(function() {

        if ($(this).attr("id") != null)
            itemDetailsChanges.removedSizes.push($(this).val());

        $(this).remove();
    });
}

function saveOrderDetails(id, btn) {

    var orderRow = $(btn).closest("tr");

    var status = orderRow.find(".status").val();

    $.ajax({
        url: saveOrderUrl,
        cache: false,
        data: { id: id, status: status },
        success: function(data) {
  
            alert("Готово!");
        },
        error: function() {

        }

    });

}

//Сохранить изменения в Товаре
function saveItemDetails(itemId) {
    var data = {};

    data.itemId = itemId;
    data.Name = $("#tbName").val();
    data.Price = $("#tbPrice").val();
    data.PriceOpt = $("#tbPriceOpt").val();
    data.DescriptionCommon = $("#tbDescriptionCommon").val();
    data.DescriptionMaterial = $("#tbDescriptionMaterial").val();

//Цвета
    itemDetailsChanges.addedColors = new Array();
    $("#lbColors option[value=0]").each(function() { itemDetailsChanges.addedColors.push($(this).html()); });

    data.addedColors = itemDetailsChanges.addedColors;
    data.removedColors = itemDetailsChanges.removedColors;

    //Размеры
    itemDetailsChanges.addedSizes = new Array();
    $("#lbSizes option[value=0]").each(function() { itemDetailsChanges.addedSizes.push($(this).html()); });

    data.addedSizes = itemDetailsChanges.addedSizes;
    data.removedSizes = itemDetailsChanges.removedSizes;
    
    data.categoryId = $("#itemDetailsHolder #ddlCategory").val();
    data.status = $("#ddlStatus").val();



   

    
    jQuery.ajaxSettings.traditional = true;

    $.ajax({
        url: saveItemDetailsUrl,
        cache: false,
        data: data,

        

        success: function(data) {

            //alert(11);
        },
        error: function() {

        }

    });
}

function showItemDetailsTab(tabNumber) {
    $("#itemDetails .tab").removeClass("selected");
    $("#itemDetails .tab").eq(tabNumber).addClass("selected");

    $("#itemDetails .tabList li").removeClass("selected");
    $("#itemDetails .tabList li").eq(tabNumber).addClass("selected");
}