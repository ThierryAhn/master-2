package properties;

import java.io.FileInputStream;
import java.io.IOException;
import java.util.Properties;

/**
 * Singleton Class Configuration which helps to get company and actions files directory
 * @author ABALINE & AHOUNOU
 * 27 janv. 2014
 */
public final class Configuration {
	
	/**
	 * Instance of configuration
	 */
	private static Configuration instance;
	
	/**
	 * Company directory name
	 */
	private String companyDirectoryName;
	
	/**
	 * Action directory name
	 */
	private String actionDirectoryName;
	
	
	
	private Configuration(){

		Properties properties = new Properties();
		FileInputStream in = null;
		try {
			in = new FileInputStream("resources/config.properties");
			properties.load(in);

		} catch (IOException ex){
			ex.printStackTrace();
		}
		companyDirectoryName = properties.getProperty("COMPANY_DIRECTORY");
		actionDirectoryName = properties.getProperty("ACTION_DIRECTORY");
	}
	
	/**
	 * Return instance of Configuration
	 * @return
	 */
	public static Configuration getInstance(){
		
		if(instance == null){
			instance = new Configuration();
		}	
		
		return instance;
	}
	
	/**
	 * Return the path of company files
	 * @return
	 */
	public String getCompanyDirectoryName(){
		return companyDirectoryName;
	}
	
	/**
	 * Return the path of action files
	 * @return
	 */
	public String getActionDirectoryName(){
		return actionDirectoryName;
	}
	
	
	
}
