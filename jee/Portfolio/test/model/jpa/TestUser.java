package model.jpa;
import model.services.IService;
import model.services.Service;


public class TestUser {

	public static void main(String[] args) {
		IService dao = new Service();
		
		
		User client = new User("Ahounou", "Thierry", "root", "root");
		dao.insert(client);
		
		client.setConfidenceLevel(ConfidenceLevel.PRIVILEDGED);
		dao.update(client);
		
	}

}
