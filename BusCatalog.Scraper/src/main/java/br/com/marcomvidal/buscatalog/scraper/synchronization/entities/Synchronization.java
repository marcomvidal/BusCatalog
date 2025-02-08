package br.com.marcomvidal.buscatalog.scraper.synchronization.entities;

import java.time.LocalDateTime;
import java.util.List;

import com.fasterxml.jackson.annotation.JsonManagedReference;

import jakarta.persistence.CascadeType;
import jakarta.persistence.Entity;
import jakarta.persistence.EnumType;
import jakarta.persistence.Enumerated;
import jakarta.persistence.GeneratedValue;
import jakarta.persistence.GenerationType;
import jakarta.persistence.Id;
import jakarta.persistence.OneToMany;
import lombok.Getter;
import lombok.Setter;

@Entity
public class Synchronization {
    @Id
    @GeneratedValue(strategy = GenerationType.AUTO)
    @Getter
    @Setter
    private Long id;

    @Getter
    @Setter
    private LocalDateTime createdAt;

    @Enumerated(EnumType.STRING)
    @Getter
    @Setter
    private SyncType type;

    @OneToMany(cascade = CascadeType.ALL, mappedBy = "synchronization")
    @JsonManagedReference
    @Getter
    @Setter
    private List<SynchronizedLine> lines;

    public Synchronization() {}

    public Synchronization(SyncType type) {
        this.createdAt = LocalDateTime.now();
        this.type = type;
    }
}
