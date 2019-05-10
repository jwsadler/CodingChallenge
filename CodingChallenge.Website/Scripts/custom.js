var mychart;
function getApiResults() {

    if ($("#search").val() == "") return;
    if (!$(".btn-search").hasClass("btn-disabled")) {

        $(".btn-search").addClass("btn-disabled");
        var formFieldsFormatted = {
                "type":$("#type").val(),
                "category":$("#category").val(),
                "query": $("#search").val()
            };
        

        $.ajax({
            type: 'POST',
            url: 'http://codeingchallenge/api/post/getdata',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(formFieldsFormatted),
            headers: { "cache-control": "no-cache" },
            cache: !1,
            beforeSend: function () {
                $(".hidediv").hide();
                $(".spinner-border").show();
                $(".slick-location").slick("unslick");
                if (mychart != null) {
                    mychart.destroy();
                }
            },
            success: function (result) {
                var html = "";
                try {
                    if (result.Images.totalHits > 0) {
                        $.each(result.Images.hits,
                            function(i, o) {
                                html += "<div><img src='" +
                                    o.webformatURL +
                                    "' alt='" +
                                    result.Request.query +
                                    "' style='width:640p;'/><div style='margin-top:5px;'>";
                                $.each(o.tagList,
                                    function(ii, oo) {
                                        html +=
                                            "<button type='button' class='btn btn-success btn-tag' style='margin-right:5px;' data-value='" +
                                            oo.trim() +
                                            "'>" +
                                            oo.trim() +
                                            "</button>";
                                    });
                                html += "</div></div>";
                            });

                        $(".slick-location").html(html);
                        $(".slick-location").slick({
                            dots: true,
                            infinite: false,
                            speed: 300

                        });
                    }

                    var words = [];
                        var counts = [];
                        var bgColor = [];
                        var bdColor = [];

                        $.each(result.Words.WordCounts,
                            function(i, o) {
                                words.push(o.Word);
                                counts.push(o.Count);
                                bgColor.push('rgba(255, 159, 64, 0.2)');
                                bdColor.push('rgba(255, 159, 64, 1)');
                            });

                        $(".wordcount").html(result.Words.TotalWordCount);

                        var ctx = document.getElementById('myChart');
                        myChart = new Chart(ctx,
                            {
                                type: 'bar',
                                data: {
                                    labels: words,
                                    datasets: [
                                        {
                                            label: '# of Words',
                                            data: counts,
                                            backgroundColor: bgColor,
                                            borderColor: bdColor,
                                            borderWidth: 1
                                        }
                                    ]
                                },
                                options: {
                                    scales: {
                                        yAxes: [
                                            {
                                                ticks: {
                                                    beginAtZero: true
                                                }
                                            }
                                        ]
                                    }
                                }
                            });

                    $(".definitions").html(result.Words.FormattedDefinitions);
                    
                } catch (err) {
                    console.log(err);
                } finally {
                    $(".spinner-border").hide();
                    $(".hidediv").show();

                    if (result.Images.totalHits == 0) {
                        $(".slider-box").hide();
                    }
                }

            },
            complete: function() {
                $(".btn-search").removeClass("btn-disabled");
                $(".spinner-border").hide();
            },
            error: (function (response) {
                console.log(response);
                $(".btn-search").removeClass("btn-disabled");
            })
        });
    }
}

$(document).ready(function () {
    $("body").on("click", ".btn-search", function () {
        getApiResults();
        return false;
    });


    $("body").on("click", ".btn-tag", function () {
        $("#search").val($(this).data("value"));
        $(".btn-search").trigger("click");
        return false;
    });

    $("#searchForm").submit(function (e) {
        e.preventDefault();
        getApiResults();
    });

    $(".slick-location").slick({
        dots: true,
        infinite: false,
        speed: 300

    });
});