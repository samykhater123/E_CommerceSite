
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

function readurl(input) {
    if (input.files && input.files[0]) {

        let reader = new FileReader();

        reader.onload = function (e) {
            $('img#uploadimage').attr("src", e.target.result).width(200).height(200);

        }
        reader.readAsDataURL(input.files[0]);
    }
}

