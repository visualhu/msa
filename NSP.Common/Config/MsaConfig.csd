<?xml version="1.0" encoding="utf-8"?>
<configurationSectionModel xmlns:dm0="http://schemas.microsoft.com/VisualStudio/2008/DslTools/Core" dslVersion="1.0.0.0" Id="b9b674c5-26b7-418e-ab92-fa5ce9a5fe98" namespace="NSP.Common.Config" xmlSchemaNamespace="urn:NSP.Common.Config" xmlns="http://schemas.microsoft.com/dsltools/ConfigurationSectionDesigner">
  <typeDefinitions>
    <externalType name="String" namespace="System" />
    <externalType name="Boolean" namespace="System" />
    <externalType name="Int32" namespace="System" />
    <externalType name="Int64" namespace="System" />
    <externalType name="Single" namespace="System" />
    <externalType name="Double" namespace="System" />
    <externalType name="DateTime" namespace="System" />
    <externalType name="TimeSpan" namespace="System" />
  </typeDefinitions>
  <configurationElements>
    <configurationSection name="MsaConfiguration" codeGenOptions="Singleton, XmlnsProperty" xmlSectionName="msaConfiguration">
      <elementProperties>
        <elementProperty name="ApplicationSetting" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="applicationSetting" isReadOnly="false">
          <type>
            <configurationElementMoniker name="/b9b674c5-26b7-418e-ab92-fa5ce9a5fe98/ApplicationSettingElement" />
          </type>
        </elementProperty>
        <elementProperty name="CommandQueue" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="commandQueue" isReadOnly="false">
          <type>
            <configurationElementMoniker name="/b9b674c5-26b7-418e-ab92-fa5ce9a5fe98/MessageQueueConfigurationElement" />
          </type>
        </elementProperty>
        <elementProperty name="EventQueue" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="eventQueue" isReadOnly="false">
          <type>
            <configurationElementMoniker name="/b9b674c5-26b7-418e-ab92-fa5ce9a5fe98/MessageQueueConfigurationElement" />
          </type>
        </elementProperty>
        <elementProperty name="MongoEventStore" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="mongoEventStore" isReadOnly="false">
          <type>
            <configurationElementMoniker name="/b9b674c5-26b7-418e-ab92-fa5ce9a5fe98/MongoEventStoreElement" />
          </type>
        </elementProperty>
        <elementProperty name="Services" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="services" isReadOnly="false">
          <type>
            <configurationElementCollectionMoniker name="/b9b674c5-26b7-418e-ab92-fa5ce9a5fe98/ServiceElementCollection" />
          </type>
        </elementProperty>
      </elementProperties>
    </configurationSection>
    <configurationElement name="ApplicationSettingElement">
      <attributeProperties>
        <attributeProperty name="Url" isRequired="true" isKey="true" isDefaultCollection="false" xmlName="url" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/b9b674c5-26b7-418e-ab92-fa5ce9a5fe98/String" />
          </type>
        </attributeProperty>
      </attributeProperties>
    </configurationElement>
    <configurationElement name="MongoEventStoreElement">
      <attributeProperties>
        <attributeProperty name="ConnectionString" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="connectionString" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/b9b674c5-26b7-418e-ab92-fa5ce9a5fe98/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="Database" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="database" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/b9b674c5-26b7-418e-ab92-fa5ce9a5fe98/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="CollectionName" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="collectionName" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/b9b674c5-26b7-418e-ab92-fa5ce9a5fe98/String" />
          </type>
        </attributeProperty>
      </attributeProperties>
    </configurationElement>
    <configurationElement name="MessageQueueConfigurationElement">
      <attributeProperties>
        <attributeProperty name="ConnectionUri" isRequired="true" isKey="true" isDefaultCollection="false" xmlName="connectionUri" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/b9b674c5-26b7-418e-ab92-fa5ce9a5fe98/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="ExchangeName" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="exchangeName" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/b9b674c5-26b7-418e-ab92-fa5ce9a5fe98/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="QueueName" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="queueName" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/b9b674c5-26b7-418e-ab92-fa5ce9a5fe98/String" />
          </type>
        </attributeProperty>
      </attributeProperties>
    </configurationElement>
    <configurationElementCollection name="ServiceElementCollection" xmlItemName="serviceElement" codeGenOptions="Indexer, AddMethod, RemoveMethod, GetItemMethods">
      <itemType>
        <configurationElementMoniker name="/b9b674c5-26b7-418e-ab92-fa5ce9a5fe98/ServiceElement" />
      </itemType>
    </configurationElementCollection>
    <configurationElement name="ServiceElement">
      <attributeProperties>
        <attributeProperty name="Type" isRequired="true" isKey="true" isDefaultCollection="false" xmlName="type" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/b9b674c5-26b7-418e-ab92-fa5ce9a5fe98/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="InstanceId" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="instanceId" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/b9b674c5-26b7-418e-ab92-fa5ce9a5fe98/String" />
          </type>
        </attributeProperty>
      </attributeProperties>
      <elementProperties>
        <elementProperty name="Settings" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="settings" isReadOnly="false">
          <type>
            <configurationElementCollectionMoniker name="/b9b674c5-26b7-418e-ab92-fa5ce9a5fe98/SettingElementCollection" />
          </type>
        </elementProperty>
        <elementProperty name="LocalCommandQueue" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="localCommandQueue" isReadOnly="false">
          <type>
            <configurationElementMoniker name="/b9b674c5-26b7-418e-ab92-fa5ce9a5fe98/MessageQueueConfigurationElement" />
          </type>
        </elementProperty>
        <elementProperty name="LocalEventQueue" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="localEventQueue" isReadOnly="false">
          <type>
            <configurationElementMoniker name="/b9b674c5-26b7-418e-ab92-fa5ce9a5fe98/MessageQueueConfigurationElement" />
          </type>
        </elementProperty>
      </elementProperties>
    </configurationElement>
    <configurationElementCollection name="SettingElementCollection" xmlItemName="settingElement" codeGenOptions="Indexer, AddMethod, RemoveMethod, GetItemMethods">
      <itemType>
        <configurationElementMoniker name="/b9b674c5-26b7-418e-ab92-fa5ce9a5fe98/SettingElement" />
      </itemType>
    </configurationElementCollection>
    <configurationElement name="SettingElement">
      <attributeProperties>
        <attributeProperty name="Key" isRequired="true" isKey="true" isDefaultCollection="false" xmlName="key" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/b9b674c5-26b7-418e-ab92-fa5ce9a5fe98/Single" />
          </type>
        </attributeProperty>
        <attributeProperty name="Value" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="value" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/b9b674c5-26b7-418e-ab92-fa5ce9a5fe98/Single" />
          </type>
        </attributeProperty>
      </attributeProperties>
    </configurationElement>
  </configurationElements>
  <propertyValidators>
    <validators />
  </propertyValidators>
</configurationSectionModel>