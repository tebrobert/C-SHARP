namespace Machine_Application {
    public class Customer {
        private Wallet Wallet;
        
        public Customer(Wallet wallet) {
            Wallet = wallet;
        }
        
        public Wallet GetWallet() {
            return Wallet;
        }
    }
}
