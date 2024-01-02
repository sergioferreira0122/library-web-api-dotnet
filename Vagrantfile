Vagrant.configure("2") do |config|
  config.vm.box = "gusztavvargadr/sql-server"

  config.vm.network "forwarded_port", guest: 1433, host: 1433
  config.vm.network "private_network", ip: "192.168.33.10"

  config.vm.hostname = "APPDB"

   config.vm.provider "vmplayer" do |vm|
     vm.gui = true

     vm.memory = "2048"
   end
end
