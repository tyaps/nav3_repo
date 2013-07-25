$(document).ready(

    function() {
        $(".slideShowRight").bind("click", function() {

            var scrollRotator = $(this).parent().find(".slideShowRotator");
            var item = scrollRotator.find(".items li:eq(0)");
            var scrollStep = item.outerWidth() + parseInt(item.css("margin-right"), 10);
        
            scrollStep = '+=' + scrollStep;

            scrollRotator.animate({
                scrollLeft: scrollStep

            }, 100)

        });

        $(".slideShowLeft").bind("click", function() {

            var scrollRotator = $(this).parent().find(".slideShowRotator");
            var item = scrollRotator.find(".items li:eq(0)");
            var scrollStep = item.outerHeight() + parseInt(item.css("margin-right"), 10);

            scrollStep = '-=' + scrollStep;

            scrollRotator.animate({
                scrollLeft: scrollStep

            }, 100)

        });

    }
)

    function img_expand(element) {

        var tX = 'main_content ' + getRandomInt(-100, 100) + 'px'
        var tY = 'main_content ' + getRandomInt(-100, 100) + 'px'

        return hs.expand(element, {targetX: tX, targetY: tY})
    }
    
    function getRandomInt(min, max)
    {
      return Math.floor(Math.random() * (max - min + 1)) + min;
  }

  function purchase(itemId) {

      if ($("#purchaseAdded").length==1)
         return;

    
//TODO: Проверять на предмет, если отсутствует цвет-размер у итема
     $.ajax({
         url: purchaseUrl,
         cache: false,
         data: {
             id: itemId,
             colorId: $("#ddlColor").val(),
             sizeId: $("#ddlSize").val()
         },
         success: function(data) {
            $("#main_content").append(data);
         },
         error: function() {

         }

     });
  
  }