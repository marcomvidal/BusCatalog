# Build stage
FROM maven:3.9.9-eclipse-temurin-21-alpine AS builder
WORKDIR /app

# Copy pom.xml & dependencies
COPY pom.xml .
COPY src/main/resources ./src/main/resources

# Download dependencies
RUN mvn dependency:resolve -Dclassifier=bin

# Copy rest of the application code
COPY src /app/src

# Build the project
RUN mvn clean package -DskipTests

# Runtime stage
FROM eclipse-temurin:21.0.5_11-jdk-alpine
WORKDIR /app

# Copy the JAR file from the builder stage
COPY --from=builder /app/target/*.jar app.jar

EXPOSE 8080

ENTRYPOINT ["java", "-jar", "app.jar"]