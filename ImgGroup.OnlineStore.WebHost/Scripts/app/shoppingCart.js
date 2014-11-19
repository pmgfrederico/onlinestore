/// <reference path="../knockout-2.0.0.debug.js" />
/// <reference path="../knockout.mapping-latest.debug.js" />
var OnlineStore;

(function (OnlineStore, toastr, ko, $) {
    var ShoppingCart = (function () {
        var self;
        function ShoppingCart(shopperId) {
            self = this;
            this.shopperId = shopperId;
            this.cartItems = ko.observableArray([]);
            this.itemCount = ko.computed(function () {
                var itemCount = this.cartItems().length;
                shoppingCartWidget.itemCount(itemCount);
                return itemCount;
            }, this);
            this.cartTotal = ko.computed(function () {
                var items = this.cartItems();
                var total = 0;

                for (var i = 0; i < items.length; i++)
                {
                    total += items[i].productPrice() * items[i].quantity();
                }

                return total;
            }, this);
            this.totalItemCount = ko.computed(function () {
                var items = this.cartItems();
                var total = 0;

                for (var i = 0; i < items.length; i++) {
                    total += items[i].quantity();
                }

                return total;
            }, this);
            this.isLoading = ko.observable(false);
        }
        ShoppingCart.prototype.initializeComponent = function (settings) {

            this.settings = settings;

            if (settings.bindingTarget) {

                ko.applyBindings(self, this.settings.bindingTarget);

                this.refresh();
            }
        };
        ShoppingCart.prototype.refresh = function () {
            $.getJSON(this.settings.getUrl, {}, function (response) {
                var viewModel = ko.mapping.fromJS(response);
                $.each(viewModel(), function (index, value) {
                    var oldValue = value.quantity();
                    $.extend(value, value.quantity.subscribe(function (newValue) {
                        if (newValue)
                        {
                            self.updateItem(value);
                        }
                        else
                        {
                            value.quantity(oldValue);
                        }
                    }));
                });
                self.cartItems(viewModel());
            });
        };
        ShoppingCart.prototype.addItem = function (item) {
            $.post(this.settings.cmdUrl,
                JSON.stringify(item),
                function (response) {
                    self.refresh();
                    toastr.success('Your cart was succesfuly updated');
                });
        };
        ShoppingCart.prototype.updateItem = function (item) {
                        
            $.post(this.settings.cmdUrl,
                JSON.stringify({
                    $type: "ImgGroup.OnlineStore.Contracts.UpdateItemCommand, ImgGroup.OnlineStore",
                    ShopperId: this.shopperId,
                    ProductId: item.productId(),
                    Quantity: item.quantity()
                }),
                function (response) {
                    self.refresh();
                    toastr.success('Your cart was succesfuly updated');
                });
        };
        ShoppingCart.prototype.removeItem = function (item) {
            $.post(this.settings.cmdUrl,
                JSON.stringify({
                    $type: "ImgGroup.OnlineStore.Contracts.RemoveItemCommand, ImgGroup.OnlineStore",
                    ShopperId: this.shopperId,
                    ProductId: item.productId()                    
                }),
                function (response) {
                    self.refresh();
                    toastr.success('Your cart was succesfuly updated');
                })
        };
        return ShoppingCart;
    })();
    OnlineStore.ShoppingCart = ShoppingCart;
})(OnlineStore || (OnlineStore = {}), window.toastr, window.ko, window.$);