import model.jpa.Address;
import model.jpa.User;
import model.jpa.ConfidenceLevel;
import model.services.IService;
import model.services.Service;


public class TestUser {

	public static void main(String[] args) {
		IService dao = new Service();
		
		Address address = new Address(38, "Republique", "Rouen", "France");
		
		User client = new User("Ahounou", "Thierry", address, "root", "root");
		dao.insert(client);
		
		client.setConfidenceLevel(ConfidenceLevel.PRIVILEDGED);
		dao.update(client);
		
	}

}
