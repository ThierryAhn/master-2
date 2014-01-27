package properties;

import java.io.FileNotFoundException;
import java.io.FileReader;
import java.io.IOException;
import java.util.Properties;

public class PortfolioProperties extends Properties {

	private static final long serialVersionUID = 1L;

	public PortfolioProperties(String propertyFileDirectory) {
		super();
		FileReader fr = null;
		try {	
			fr = new FileReader(getClass().getResource(propertyFileDirectory).getPath());
			load(fr);
		} catch (FileNotFoundException e) {
			e.printStackTrace();
		} catch (IOException e) {
			e.printStackTrace();
		} finally {
			if (fr != null) {
				try {
					fr.close();
				} catch (IOException e) {
					e.printStackTrace();
				}
			}
		}
	}
}

