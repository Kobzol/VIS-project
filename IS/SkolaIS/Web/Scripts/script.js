$(function()
{
    $("body").on("click", ".supplement-list", function()
    {
        var subjectId = $(this).siblings("input[name=subjectId]").val();
        var day = $(this).siblings("input[name=day]").val();
        var order = $(this).siblings("input[name=order]").val();

        var availTeachers = $(".available-teachers");
        availTeachers.find(".info").html("Načítám...");

        $.ajax({
            url: "/Schedule/ShowSupplement",
            method: "POST",
            data: {
                subjectId: subjectId,
                day: day,
                order: order
            },
            success: function(response)
            {
                availTeachers.find(".info").html("");

                var list = availTeachers.find(".list");
                var cancel = availTeachers.find(".cancel");

                availTeachers.find("input[name=selected-subject]").val(subjectId);
                availTeachers.find("input[name=selected-day]").val(day);
                availTeachers.find("input[name=selected-order]").val(order);

                if (response.teachers.length > 0)
                {
                    var select = $("<select/>");
                    list.show();
                    cancel.hide();

                    for (var i = 0; i < response.teachers.length; i++)
                    {
                        var option = $("<option value='" + response.teachers[i].Id + "'>" + response.teachers[i].Surname + "</option>");
                        select.append(option);
                    }

                    list.html(select);
                    list.append("<button class='supplement-send'>Add supplement</button>");
                }
                else
                {
                    cancel.show();
                    list.hide();
                }
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
            url: "/Schedule/AddSupplement",
            method: "POST",
            data: {
                subjectId: subjectId,
                day: day,
                order: order,
                teacherId: teacherId
            },
            success: function(response)
            {
                alert("Supplement was added!");
                $(".content").html("Loading...");
                $.get("/Schedule/Index", function(data)
                {
                    $(".content").html(data);
                });
            }
        });
    });

    $("body").on("click", ".cancel-send", function()
    {
        var availTeachers = $(".available-teachers");
        var subjectId = availTeachers.find("input[name=selected-subject]").val();
        var day = availTeachers.find("input[name=selected-day]").val();
        var order = availTeachers.find("input[name=selected-order]").val();

        $.ajax({
            url: "/Schedule/SupplementCancel",
            method: "POST",
            data: {
                subjectId: subjectId,
                day: day,
                order: order
            },
            success: function(response)
            {
                alert("Lesson was canceled!");
                $(".content").html("Loading...");
                $.get("/Schedule/Index", function(data)
                {
                    $(".content").html(data);
                });
            }
        });
    });
});