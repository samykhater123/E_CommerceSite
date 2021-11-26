
$(function () {
    if ($('a.confirmdeleations').last) {
        $('a.confirmdeleations').click(function () {

            if (!confirm("confirm deleation")) return false;
        })
    }

    if ($('div.alert.notification').last) {
       
            setTimeout(() => {
                $('div.alert.notification').fadeOut();
            },2000);
       
    }

});

