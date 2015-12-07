$(function()
{
    $("body").on("click", ".supplement-list", function()
    {
        var subjectId = $(this).siblings("input[name=subjectId]").val();
        var day = $(this).siblings("input[name=day]").val();
        var order = $(this).siblings("input[name=order]").val();

        $.ajax({
            url: "Schedule/ShowSupplement",
            method: "POST",
            data: {
                subjectId: subjectId,
                day: day,
                order: order
            },
            success: function(response)
            {
                var availTeachers = $(".available-teachers");
                var list = availTeachers.find(".list");
                var select = $("<select/>");

                for (var i = 0; i < response.teachers.length; i++)
                {
                    var option = $("<option value='" + response.teachers[i].id + "'>" + response.teachers[i].name + "</option>");
                    select.append(option);
                }

                availTeachers.find("input[name=selected-subject]").val(subjectId);
                availTeachers.find("input[name=selected-day]").val(day);
                availTeachers.find("input[name=selected-order]").val(order);

                list.html(select);
                list.append("<button class='supplement-send'>Přidat suplování</button>");
            }
        });
    });

    $("body").on("click", ".supplement-send", function()
    {
        var availTeachers = $(".available-teachers");
        var subjectId = availTeachers.find("input[name=selected-subject]").val();
        var day = availTeachers.find("input[name=selected-day]").val();
        var order = availTeachers.find("input[name=selected-order]").val();
        var teacherId = availTeachers.find("select").val();

        $.ajax({
            url: "Schedule/AddSupplement",
            method: "POST",
            data: {
                subjectId: subjectId,
                day: day,
                order: order,
                teacherId: teacherId
            },
            success: function(response)
            {
                alert("Suplování bylo přidáno!");
            }
        });
    });
});