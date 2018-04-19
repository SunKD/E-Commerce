// Write your JavaScript code.
$(document).ready(function(){
        $("#search").keyup(function(e){
            e.preventDefault();
            console.log(this.value)
            console.log(e);
        console.log($(e.currentTarget.form).serialize());
            $.ajax({
                url:"/search",
                method: "POST",
                data: $(e.currentTarget.form).serialize(),
                success: function(res){
                    console.log(res);
                    var strbuilder = `
                    <table class="table table-striped text-center" id="customerT">
                    <thead>
                        <tr>
                            <th class='text-center'>Customer Name</th>
                            <th class='text-center'>Created Date</th>
                            <th class='text-center'>Actions</th>
                        </tr>
                    </thead>
                    <tbody>
                    `
                res.forEach(e => {
                    strbuilder += `
                    <tr>
                        <td>${e["name"]}</td>
                        <td>${e["displayDate"]}</td>
                        <td><a href="remove/${e.customerid}" class="btn btn-danger">Remove</a></td>
                    </tr>
                    `
                    });
                    strbuilder += `</tbody></table>`
                $("#tablerow").html(strbuilder);
                }
            });
        });
    });