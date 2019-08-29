$(document).ready(function () {
   var id = 0;

   


    $('body').on('click', '.btn-danger', (function () {
        id = ($(this).data('id'));
    }));



    $('body').on('click', '.edit-button', (function () {
      
         var productId = $(this).attr('data-id');
        
        $.ajax({
            url: '/Home/GetProductById',
            method: 'post',
            data: { 'id': productId },

            success: function (response) {
                
                $('#ProductIDForEdit').val(response.ID);
                $('#ProductNameForEdit').val(response.Name);
                $('#ProductPriceForEdit').val(response.Price);

            }
        });
    }));
   

   
    $('#editButton').click(function () {
        var id = $('#ProductIDForEdit').val();
        var name = $('#ProductNameForEdit').val();
        var price = $('#ProductPriceForEdit').val();
        $.ajax({
            url: '/Home/Edit',
            method: 'post',
            data: { 'ProductId': id, 'ProductName': name, 'ProductPrice': price },
            success: function (response) {
                
                $('tr[data-productId=' + id + ']>td').eq(1).text(name);
                $('tr[data-productId=' + id + ']>td').eq(2).text(price+'$');
                $('.btn-secondary').trigger('click');
                
            }

        })
    })

 


    



    $('#add').click(function () {
        
        $('#MessageContent').html('');
        $('#ProductName').val('');
        $('#ProductPrice').val('');
    });

    $('#delete').click(function () {
     
        $.ajax({
            
            url: '/Home/Delete',
            method: 'post',
            data: { 'ProductId': id },
            success: function (response) {
               
                $('tr[data-productId=' + response+']').hide();
                $('#deleteModal').trigger('click');
                
            }
        });
    });

    $('#PriceBtn').click(function () {
        var name = $('#ProductName').val();
        var price = $('#ProductPrice').val();
    
        if (name == "" || price <= 0) {
            $('#MessageContent').html('შეავსეთ ორივე ველის ინფორმაცია');
        }
        else {
            $.ajax({
                url: '/Home/Catalog',
                method: 'post',
                data: { 'ProductName': name, 'ProductPrice': price },
                success: function (response) {
                    var manualTr = '<tr  data-productId=' + response.ID + ' > <td>' + response.ID + '</td> <td>' + response.Name + '</td> <td>' + response.Price + '$' + '</td> <td><button data-id=' + response.ID + ' class="btn btn-danger" data-toggle="modal" data-target="#deleteModal"> წაშლა</button >' + ' ' + ' <button data-id=' + response.ID + ' class="btn btn-default edit-button" data-toggle="modal" data-target="#editModal">შეცვლა</button>   </td> </tr > '
                    $('table').find('tr:last').after(manualTr);
                    $('#CloseBtn').trigger('click');
                    $('#ProductName').val('');
                    $('#ProductPrice').val('');
                },
                error: function () {
                    $('#MessageContent').html('დაფიქსირდა შცდომა');
                }
            });
        }

        });
  
});