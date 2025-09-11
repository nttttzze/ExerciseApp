import { ImageBackground, View, Text } from "react-native";
import backgroundImg from "@/assets/images/background.jpg";
import { styles } from "./index";
import React from "react";

function ContactPage() {
  return (
    <View style={styles.container}>
      <ImageBackground
        style={styles.image}
        resizeMode="cover"
        source={backgroundImg}
      >
        <Text style={styles.title}>Contact Us</Text>
        <View style={styles.contactContainer}>
          <Text style={{ color: "black" }}>
            Email: Nataniel112233@gmail.com {"\n"} Address: Malmö address 123
            456
          </Text>
        </View>
      </ImageBackground>
    </View>
  );
}
export default ContactPage;
