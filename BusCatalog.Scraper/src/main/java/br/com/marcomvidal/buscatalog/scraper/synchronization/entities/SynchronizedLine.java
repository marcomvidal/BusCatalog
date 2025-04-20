package br.com.marcomvidal.buscatalog.scraper.synchronization.entities;

import com.fasterxml.jackson.annotation.JsonBackReference;

import jakarta.persistence.Entity;
import jakarta.persistence.GeneratedValue;
import jakarta.persistence.GenerationType;
import jakarta.persistence.Id;
import jakarta.persistence.JoinColumn;
import jakarta.persistence.ManyToOne;
import lombok.Getter;
import lombok.Setter;

@Entity
@Getter
@Setter
public class SynchronizedLine {
    @Id
    @GeneratedValue(strategy = GenerationType.AUTO)
    private Long id;

    private String identification;

    @ManyToOne
    @JsonBackReference
    @JoinColumn(name = "synchronization_id")
    private Synchronization synchronization;

    public SynchronizedLine() {}

    public SynchronizedLine(String identification, Synchronization synchronization) {
        this.identification = identification;
        this.synchronization = synchronization;
    }
}
